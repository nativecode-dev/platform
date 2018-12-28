namespace NativeCode.Node.Media
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NativeCode.Core.Extensions;
    using NativeCode.Node.Media.Data;
    using NativeCode.Node.Media.Data.Hosting;
    using NativeCode.Node.Media.Data.Services.Storage;
    using NativeCode.Node.Media.Services.Storage;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddMediaServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            return services.AddScoped<IFileService, FileService>()
                .AddScoped<IMountService, MountService>()
                .AddDbContext<MediaDataContext>(options);
        }

        public static IServiceCollection AddMediaStorageMonitor(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddOption<StorageMonitorOptions>(configuration)
                .AddScoped<IMonitorService, MonitorService>()
                .AddHostedService<StorageMonitorService>();
        }
    }
}
