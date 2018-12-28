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
        public static IServiceCollection AddMediaServices(this IServiceCollection services, IConfiguration configuration,
            Action<DbContextOptionsBuilder> dboptions)
        {
            services.AddOption<StorageMonitorOptions>(configuration);
            services.AddDbContext<MediaDataContext>(dboptions);

            services.AddScoped<IFileMonitorService, FileMonitorService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IMountService, MountService>();
            services.AddHostedService<StorageMonitorService>();

            return services;
        }
    }
}
