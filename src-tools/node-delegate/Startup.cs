namespace node_delegate
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using AutoMapper;

    using IdentityServer4.AccessTokenValidation;

    using Microsoft.AspNetCore.Authentication;
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
    using NativeCode.Core.Web;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Core.WebHosting;

    using node_delegate.Options;
    using node_delegate.Services;

    public class Startup : AspNetStartup<NodeOptions>
    {
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggingFactory,
            ILogger<AspNetStartup<NodeOptions>> logger)
            : base(configuration, hostingEnvironment, loggingFactory, logger)
        {
        }

        protected override string AppName => "Delegate";

        protected override string AppProduct => "Platform";

        protected override string AppVersion => "v1";

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(
                options =>
                    {
                        options.AddPolicy(
                            this.Options.ApiScope,
                            configure =>
                                {
                                    configure.RequireAuthenticatedUser();
                                    configure.RequireScope(this.Options.ApiScope);
                                });
                    });

            services.AddAutoMapper();

            services.AddOption<DockerClientOptions>(this.Configuration);
            services.AddScoped<IProxyService, ProxyService>();
            services.Configure<DockerClientOptions>(
                options =>
                    {
                        options.Credentials = null;
                        options.Url = new Uri("tcp://localhost:2375");
                        options.Version = null;
                    });

            return base.ConfigureServices(services);
        }

        protected override IApplicationBuilder ConfigureMiddleware(IApplicationBuilder app)
        {
            if (this.HostingEnvironment.IsProduction())
            {
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseCors()
                .UseForwardedHeaders()
                .UseMvcWithDefaultRoute()
                .UseStaticFiles()
                .UseStatusCodePages(
                    context =>
                        {
                            var response = context.HttpContext.Response;

                            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                            {
                                response.Redirect("/account/login");
                            }

                            return Task.CompletedTask;
                        });

            return app;
        }

        protected override IMvcCoreBuilder ConfigureMvc(IMvcCoreBuilder builder)
        {
            return builder.AddCacheTagHelper()
                .AddCookieTempDataProvider()
                .AddDataAnnotations()
                .AddModelValidator()
                .AddRazorPages()
                .AddRazorViewEngine()
                .AddViews()
                .AddMvcOptions(
                    options =>
                        {
                            options.Filters.Add(new AuthorizeFilter(ScopePolicy.Create(this.Options.ApiScope)));
                            options.Filters.Add(new ProducesAttribute("application/json"));
                        });
        }

        protected override AuthenticationBuilder CreateAuthenticationBuilder(IServiceCollection services)
        {
            return services.AddAuthentication(
                options =>
                    {
                        options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    });
        }
    }
}
