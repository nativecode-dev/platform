namespace NativeCode.Clients.Radarr.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRadarrClient(this IServiceCollection services)
        {
            return services.AddSingleton<IClientFactory<RadarrClient>, RadarrClientFactory>();
        }
    }
}