namespace NativeCode.Node.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddIrcWatch(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<IrcWatcher>();
            services.Configure<IrcWatchOptions>(configuration.GetSection(nameof(IrcWatchOptions)));
            return services;
        }
    }
}