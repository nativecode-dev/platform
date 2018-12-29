namespace NativeCode.Core.Storage
{
    using Microsoft.Extensions.DependencyInjection;

    public static class RemoteFileStoreExtensions
    {
        public static IServiceCollection AddRemoteFileStore(this IServiceCollection services)
        {
            services.AddScoped<IRemoteFileStore, RemoteFileStore>();

            return services;
        }
    }
}
