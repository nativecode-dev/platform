namespace node_processor
{
    using System;
    using System.IO;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NativeCode.Clients;
    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Sonarr;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Messaging.Extensions;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Services;
    using NativeCode.Node.Services.Watchers;
    using Serilog;

    internal class Program
    {
        internal const string AppName = "Processor";

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
                    services.AddOption<ElasticSearchOptions>(context.Configuration, out var elasticsearch);
                    services.AddSerilog(context.Configuration, elasticsearch.Url);
                    Log.Logger.Information("Startup: {@node}", node);

                    services.AddDistributedRedisCache(options =>
                    {
                        options.Configuration = node.RedisHost;
                        options.InstanceName = AppName;
                    });

                    services.AddAutoMapper(config => config.AddProfile<DefaultMapperProfile>());
                    services.AddRabbitServices(context.Configuration);
                    services.AddObjectSerializer();

                    services.AddOption<MovieWatcherOptions>(context.Configuration);
                    services.AddOption<SeriesWatcherOptions>(context.Configuration);

                    services.AddTransient<IClientFactory<RadarrClient>, RadarrClientFactory>();
                    services.AddTransient<IClientFactory<SonarrClient>, SonarrClientFactory>();

                    services.AddHostedService<MovieWatcher>();
                    services.AddHostedService<SeriesWatcher>();
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConsoleLifetime()
                .UseEnvironment(Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT"))
                .UseKeyValueConfig(Name, AppName)
                .UseSerilog();
        }
    }
}
