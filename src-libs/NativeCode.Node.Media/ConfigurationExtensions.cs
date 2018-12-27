namespace NativeCode.Node.Media
{
    using System;
    using Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddMediaServices(this IServiceCollection services, Action<DbContextOptionsBuilder> dboptions)
        {
            services.AddDbContext<MediaDataContext>(dboptions);

            services.AddScoped<IFileMonitorService, FileMonitorService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IMountService, MountService>();
            services.AddHostedService<MediaDiskMonitorService>();

            return services;
        }
    }
}
