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
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Services;
    using Serilog;
    using Serilog.Exceptions;
    using Serilog.Sinks.Elasticsearch;

    internal class Program
    {
        internal const string ConfigRoot = "tcp://etcd:2379/NativeCode/Node/Watcher";

        internal const string ConfigShared = "tcp://etcd:2379/NativeCode/Shared";

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
                    var defaultConfig = $"{ConfigRoot}/{Version}";
                    var environmentConfig = $"{ConfigRoot}/{env}";
                    var machineConfig = $"{ConfigRoot}/{Environment.MachineName}";
                    var sharedConfig = $"{ConfigShared}";

                    builder.AddJsonFile("appsettings.json", false, true);
                    builder.AddJsonFile($"appsettings.{env}.json", true, true);
                    builder.AddEtcdConfig(sharedConfig, defaultConfig, environmentConfig, machineConfig);
                    builder.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, builder) => builder.AddSerilog())
                .ConfigureServices((context, services) =>
                {
                    services.AddOption<NodeOptions>(context.Configuration, out var node);
                    services.AddOption<ElasticSearchOptions>(context.Configuration, out var elasticsearch);

                    var esconfig = new ElasticsearchSinkOptions(new Uri(elasticsearch.Url))
                    {
                        AutoRegisterTemplate = true
                    };

                    Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithExceptionDetails()
                        .ReadFrom.Configuration(context.Configuration)
                        .WriteTo.Elasticsearch(esconfig)
                        .WriteTo.Console()
                        .WriteTo.Debug()
                        .WriteTo.Trace()
                        .CreateLogger();

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
