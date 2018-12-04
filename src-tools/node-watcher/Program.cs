namespace node_watcher
{
    using System;
    using System.IO;
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Configuration;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Messaging.Extensions;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Services;

    internal class Program
    {
        internal const string ConfigRoot = "tcp://etcd:2379/NativeCode/Node/Watcher";

        internal const string Name = "watcher";

        internal const string Version = "v1";

        private static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName;
                    var defaultConfig = $"{ConfigRoot}/{Version}";
                    var environmentConfig = $"{ConfigRoot}/{env}";
                    var machineConfig = $"{ConfigRoot}/{Environment.MachineName}";

                    builder.AddJsonFile("appsettings.json", false, true);
                    builder.AddEtcdConfig(defaultConfig, environmentConfig, machineConfig);
                    builder.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, options) =>
                {
                    options.Services.AddLogging();
                    options.AddConfiguration(context.Configuration.GetSection("Logging"));
                    options.AddConsole();
                    options.AddDebug();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddOption<NodeOptions>(context.Configuration, out var node);

                    services.AddDistributedRedisCache(options =>
                    {
                        options.Configuration = node.RedisHost;
                        options.InstanceName = Name;
                    });

                    services.AddAutoMapper();
                    services.AddRabbitServices(context.Configuration);
                    services.AddIrcWatch(context.Configuration);
                })
                .UseConsoleLifetime();
        }
    }
}
