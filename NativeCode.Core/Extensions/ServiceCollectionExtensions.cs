namespace NativeCode.Core.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseObjectSerializer(this IServiceCollection services)
        {
            return services.AddTransient<IObjectSerializer, JsonObjectSerializer>();
        }
    }
}
