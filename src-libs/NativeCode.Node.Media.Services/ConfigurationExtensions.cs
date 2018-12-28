namespace NativeCode.Node.Media.Services
{
    using System;
    using Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NativeCode.Core.Extensions;
    using Storage;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddMediaServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            return services
                .AddScoped<IFileService, FileService>()
                .AddScoped<IMountService, MountService>()
                .AddDbContext<MediaDataContext>(options);
        }

        public static IServiceCollection AddMediaStorageMonitor(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddOption<StorageMonitorOptions>(configuration)
                .AddScoped<IMonitorService, MonitorService>()
                .AddHostedService<StorageMonitorService>();
        }
    }
}
