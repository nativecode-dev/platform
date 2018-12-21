namespace node_scheduler
{
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NativeCode.Node.Core.WebHosting;

    public class Startup : AspNetStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILoggerFactory logging) : base(configuration,
            environment, logging)
        {
        }

        protected override string AppName => "Scheduler";

        protected override string AppProduct => "Platform";

        protected override string AppVersion => "v1";

        protected override AuthenticationBuilder CreateAuthenticationBuilder(IServiceCollection services)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
        }

        protected override IApplicationBuilder ConfigureMiddleware(IApplicationBuilder app)
        {
            if (this.Environment.IsProduction())
            {
                app.UseHsts();
            }
            else
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders();

            return app;
        }

        protected override IMvcCoreBuilder ConfigureMvc(IMvcCoreBuilder builder)
        {
            return builder;
        }
    }
}
