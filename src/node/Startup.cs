namespace node
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Filters;
    using Hangfire;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Mvc;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Core.WebHosting;
    using NativeCode.Node.Media;
    using NativeCode.Node.Media.Data;

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

        protected override string AppName => "Node";

        protected override string AppProduct => "Platform";

        protected override string AppVersion => "v1";

        protected override IApplicationBuilder ConfigureApp(IApplicationBuilder app)
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
                .UseHangfireDashboard(
                    "/jobs",
                    new DashboardOptions
                    {
                        AppPath = "/",
                        Authorization = new[]
                        {
                            new DashboardAuthorizationFilter(),
                        },
                    })
                .UseHangfireServer(
                    new BackgroundJobServerOptions
                    {
                        ServerName = $"{Environment.MachineName}:{this.Options.Name}:{Process.GetCurrentProcess().Id}",
                        WorkerCount = this.Options.WorkerCount,
                    })
                .UseHealthChecks("/health")
                .UseMvc()
                .UseStaticFiles()
                .UseStatusCodePages(
                    context =>
                    {
                        var response = context.HttpContext.Response;

                        if (response.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            response.Redirect("/account/login");
                        }

                        return Task.CompletedTask;
                    });

            return app;
        }

        protected override IServiceCollection ConfigureAppServices(IServiceCollection services)
        {
            services.AddOption<RedisOptions>(this.Configuration, out var redis);

            services.AddHealthChecks()
                .AddDbContextCheck<MediaDataContext>();

            services.AddMediaServices(options =>
                {
                    var connectionString = this.Configuration.GetConnectionString(nameof(MediaDataContext));
                    options.UseSqlServer(connectionString);
                })
                .AddMediaStorageMonitor(this.Configuration);

            var addresses = Dns.GetHostAddresses(redis.Host);
            services.AddHangfire(x => x.UseRedisStorage(addresses.First()
                .ToString()));

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    this.Options.ApiScope,
                    configure =>
                    {
                        configure.RequireAuthenticatedUser();
                        configure.RequireScope(this.Options.ApiScope);
                    });
            });

            return services;
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
                        var policy = ScopePolicy.Create(this.Options.ApiScope);
                        options.Filters.Add(new AuthorizeFilter(policy));
                    });
        }

        protected override AuthenticationBuilder ConfigueAuthentication(IServiceCollection services)
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
