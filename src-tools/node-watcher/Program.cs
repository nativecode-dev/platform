namespace node_watcher
{
    using System;
    using System.IO;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Messaging.Extensions;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Services;
    using Serilog;

    internal class Program
    {
        internal const string AppName = "Watcher";

        internal const string Name = "Platform";

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
                .ConfigureServices((context, services) =>
                {
                    services.AddOption<NodeOptions>(context.Configuration, out var node);
                    services.AddSerilog(context.Configuration, Program.Name);
                    Log.Logger.Information("Startup: {@node}", node);

                    services.AddDistributedRedisCache(options =>
                    {
                        options.Configuration = node.RedisHost;
                        options.InstanceName = AppName;
                    });
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddAutoMapper();
                    services.AddRabbitServices(context.Configuration);
                    services.AddIrcWatch(context.Configuration);
                })
                .UseConsoleLifetime()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment(Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT"))
                .UseKeyValueConfig(Name, AppName)
                .UseSerilog();
        }
    }
}
