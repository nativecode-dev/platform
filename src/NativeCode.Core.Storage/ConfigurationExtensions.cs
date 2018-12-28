namespace NativeCode.Core.Storage
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, SimpleStorageService>();

            return services;
        }
    }
}
