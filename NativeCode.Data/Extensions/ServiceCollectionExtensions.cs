namespace NativeCode.Data.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataCommerce(this IServiceCollection services)
        {
            return services.AddDbContext<CommerceDataContext>();
        }

        public static IServiceCollection AddDataProfiles(this IServiceCollection services)
        {
            return services.AddDbContext<ProfileDataContext>();
        }
    }
}
