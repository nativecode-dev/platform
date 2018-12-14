namespace node_watcher
{
    using System;
    using System.IO;
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NativeCode.Core.Configuration;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Messaging.Extensions;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Services;
    using Serilog;

    internal class Program
    {
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
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName;
                    var common = "tcp://etcd:2379/root/Platform/Common";
                    var options = $"tcp://etcd:2379/root/Platform/Watcher/{env}";

                    builder.AddJsonFile("appsettings.json", false, true);
                    builder.AddJsonFile($"appsettings.{env}.json", true, true);
                    builder.AddEtcdConfig(common, options);
                    builder.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, builder) => builder.AddSerilog())
                .ConfigureServices((context, services) =>
                {
                    services.AddOption<NodeOptions>(context.Configuration, out var node);
                    services.AddOption<ElasticSearchOptions>(context.Configuration, out var elasticsearch);
                    services.AddSerilog(context.Configuration, elasticsearch.Url);

                    Log.Logger.Information("Startup: {@node}", node);

                    services.AddDistributedRedisCache(options =>
                    {
                        options.Configuration = node.RedisHost;
                        options.InstanceName = Name;
                    });

                    services.AddAutoMapper();
                    services.AddRabbitServices(context.Configuration);
                    services.AddIrcWatch(context.Configuration);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConsoleLifetime()
                .UseEnvironment(Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT"))
                .UseSerilog();
        }
    }
}
