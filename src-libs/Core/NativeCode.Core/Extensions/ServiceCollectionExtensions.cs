namespace NativeCode.Core.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serialization;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddObjectSerializer(this IServiceCollection services)
        {
            return services.AddTransient<IObjectSerializer, JsonObjectSerializer>();
        }

        public static IServiceCollection AddOption<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class
        {
            return services.Configure<T>(configuration.GetSection(typeof(T).Name));
        }

        public static IServiceCollection AddOption<T>(this IServiceCollection services, IConfiguration configuration, out T options)
            where T : class, new()
        {
            services.AddOption<T>(configuration);

            options = new T();
            configuration.Bind(typeof(T).Name, options);

            return services;
        }
    }
}
