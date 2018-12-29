namespace NativeCode.Core.Messaging.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Options;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddRabbitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitOptions>(configuration.GetSection(nameof(RabbitOptions)));

            services.AddTransient<IQueueSerializer, QueueSerializer>();
            services.AddSingleton<IQueueManager, RabbitQueueManager>();

            return services;
        }
    }
}
