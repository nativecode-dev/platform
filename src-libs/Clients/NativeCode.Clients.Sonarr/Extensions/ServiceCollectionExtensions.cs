namespace NativeCode.Clients.Sonarr.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSonarrClient(this IServiceCollection services)
        {
            return services.AddSingleton<IClientFactory<SonarrClient>, SonarrClientFactory>();
        }
    }
}
