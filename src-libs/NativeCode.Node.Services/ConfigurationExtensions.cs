namespace NativeCode.Node.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Watchers;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddIrcWatch(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<IrcWatcher>();
            services.Configure<IrcWatcherOptions>(configuration.GetSection(nameof(IrcWatcherOptions)));
            return services;
        }
    }
}
