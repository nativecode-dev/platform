namespace node
{
    using System;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Extensions;
    using NativeCode.Node.Core;
    using Newtonsoft.Json.Converters;
    using NSwag;
    using Serilog;

    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory logging)
        {
            this.Configuration = configuration;
            this.HostEnv = hostingEnvironment;
            this.Logging = logging;
        }

        protected IConfiguration Configuration { get; }

        protected IHostingEnvironment HostEnv { get; }

        protected ILoggerFactory Logging { get; }

        public void Configure(IApplicationBuilder app)
        {
            if (this.HostEnv.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders();
            app.UseMvc();
            app.UseSwagger(config =>
            {
                config.PostProcess = (settings, c) =>
                {
                    settings.Schemes.Clear();
                    settings.Schemes.Add(SwaggerSchema.Https);
                };
            });
            app.UseSwaggerUi3();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOption<NodeOptions>(this.Configuration, out var node);
            services.AddSerilog(this.Configuration, Program.Name);

            Log.Logger.Information("Startup: {@node}", node);

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = node.RedisHost;
                options.InstanceName = Program.AppName;
            });

            services.AddSerilog(this.Configuration, Program.AppName);

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters()
                .AddMvcOptions(options =>
                {
                    var policy = ScopePolicy.Create(node.ApiScope);
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddAuthorization(options => { options.AddPolicy(node.ApiScope, configure => configure.RequireAuthenticatedUser()); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiName = node.ApiName;
                    options.ApiSecret = node.ApiSecret;
                    options.Authority = node.Authority;
                    options.RequireHttpsMetadata = false;
                    options.JwtValidationClockSkew = TimeSpan.FromMinutes(10);
                });

            services
                .AddCors()
                .AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerDocument(options =>
            {
                options.DocumentName = Program.Version;
                options.Title = Program.AppName;
            });

            return services.BuildServiceProvider();
        }
    }
}
