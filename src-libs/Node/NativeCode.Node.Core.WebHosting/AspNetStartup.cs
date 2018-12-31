namespace NativeCode.Node.Core.WebHosting
{
    using System;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Extensions;
    using NSwag;
    using NSwag.SwaggerGeneration.Processors.Security;
    using Options;
    using Serilog;

    public abstract class AspNetStartup<TOptions> : IStartup
        where TOptions : NodeOptions, new()
    {
        protected AspNetStartup(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggingFactory,
            ILogger<AspNetStartup<TOptions>> logger)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
            this.Logger = logger;
            this.LoggingFactory = loggingFactory;

            this.Configuration.Bind(typeof(TOptions).Name, this.Options);
        }

        protected abstract string AppName { get; }

        protected abstract string AppProduct { get; }

        protected abstract string AppVersion { get; }

        protected IConfiguration Configuration { get; }

        protected IHostingEnvironment HostingEnvironment { get; }

        protected ILogger<AspNetStartup<TOptions>> Logger { get; }

        protected ILoggerFactory LoggingFactory { get; }

        protected TOptions Options { get; } = new TOptions();

        public void Configure(IApplicationBuilder app)
        {
            this.ConfigureApp(
                app.UseMvc()
                    .UseSwagger(
                        config =>
                        {
                            config.PostProcess = (settings, c) =>
                            {
                                settings.Schemes.Clear();
                                settings.Schemes.Add(SwaggerSchema.Https);
                            };
                        })
                    .UseSwaggerUi3());
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOption<TOptions>(this.Configuration);
            services.AddOption<RedisOptions>(this.Configuration, out var redis);
            services.AddSerilog(this.Configuration, this.AppName);

            Log.Logger.Information(
                "Startup: {@node}",
                new
                {
                    this.Options.ApiName,
                    this.Options.ApiScope,
                    ApiSecret = this.Options.ApiSecret.ToSecretString(),
                    this.Options.Authority,
                    this.Options.ClientId,
                    ClientSecret = this.Options.ClientSecret.ToSecretString(),
                    this.Options.ClockSkew,
                    Redis = redis.Host,
                    this.Options.Name,
                });

            services.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = redis.Host;
                    options.InstanceName = this.AppName;
                });

            services.AddSerilog(this.Configuration, this.AppName);

            var mvc = services.AddMvcCore()
                .AddCors()
                .AddApiExplorer()
                .AddAuthorization()
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddMvcOptions(
                    options =>
                    {
                        var policy = ScopePolicy.Create(this.Options.ApiScope);
                        options.Filters.Add(new AuthorizeFilter(policy));
                    })
                .AddAuthorization(options => options.AddPolicy(this.Options.ApiScope, configure => configure.RequireAuthenticatedUser()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            this.ConfigureMvc(mvc);

            this.ConfigueAuthentication(services)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = this.Options.ApiName;
                        options.ApiSecret = this.Options.ApiSecret;
                        options.Authority = this.Options.Authority;
                        options.RequireHttpsMetadata = false;
                        options.JwtValidationClockSkew = this.Options.ClockSkew;
                    });

            services.AddSwaggerDocument(
                options =>
                {
                    options.DocumentName = this.AppVersion;
                    options.Title = this.AppName;

                    options.DocumentProcessors.Add(
                        new SecurityDefinitionAppender(
                            IdentityServerAuthenticationDefaults.AuthenticationScheme,
                            new SwaggerSecurityScheme
                            {
                                Type = SwaggerSecuritySchemeType.ApiKey,
                                Name = "Authorization",
                                Description = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                                In = SwaggerSecurityApiKeyLocation.Header,
                            }));

                    var scheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.OperationProcessors.Add(new OperationSecurityScopeProcessor(scheme));

                    options.OperationProcessors.Add(new UnauthorizedOperationProcessor());
                });

            return this.ConfigureAppServices(services)
                .BuildServiceProvider();
        }

        protected abstract IApplicationBuilder ConfigureApp(IApplicationBuilder app);

        protected abstract IServiceCollection ConfigureAppServices(IServiceCollection services);

        protected abstract IMvcCoreBuilder ConfigureMvc(IMvcCoreBuilder builder);

        protected abstract AuthenticationBuilder ConfigueAuthentication(IServiceCollection services);
    }
}
