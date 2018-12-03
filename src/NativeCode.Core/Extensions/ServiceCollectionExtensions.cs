namespace NativeCode.Core.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Serialization;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseObjectSerializer(this IServiceCollection services)
        {
            return services.AddTransient<IObjectSerializer, JsonObjectSerializer>();
        }
    }
}