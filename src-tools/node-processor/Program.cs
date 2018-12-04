namespace node_processor
{
    using System;
    using System.IO;
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NativeCode.Clients;
    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Radarr.Requests;
    using NativeCode.Clients.Sonarr;
    using NativeCode.Clients.Sonarr.Requests;
    using NativeCode.Core.Configuration;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Messaging.Extensions;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Messages;
    using NativeCode.Node.Services;
    using Serilog;
    using Serilog.Sinks.Elasticsearch;
    using Protocol = NativeCode.Clients.Radarr.Requests.Protocol;

    internal class Program
    {
        internal const string ConfigRoot = "tcp://etcd:2379/NativeCode/Node/Processor";

        internal const string Name = "processor";

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
                    var env = context.HostingEnvironment.EnvironmentName = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
                    var defaultConfig = $"{ConfigRoot}/{Version}";
                    var environmentConfig = $"{ConfigRoot}/{env}";
                    var machineConfig = $"{ConfigRoot}/{Environment.MachineName}";

                    builder.AddJsonFile("appsettings.json", false, true);
                    builder.AddJsonFile($"appsettings.{env}.json", true, true);
                    builder.AddEtcdConfig(defaultConfig, environmentConfig, machineConfig);
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

                    services.AddAutoMapper(config =>
                    {
                        config.CreateMap<MovieRelease, MovieReleaseInfo>()
                            .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Link))
                            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Protocol, opt => opt.MapFrom(src => Protocol.Torrent))
                            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.UtcNow.ToString("o")))
                            .ReverseMap();

                        config.CreateMap<SeriesRelease, SeriesReleaseInfo>()
                            .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Link))
                            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Protocol, opt => opt.MapFrom(src => Protocol.Torrent))
                            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.UtcNow.ToString("o")))
                            .ReverseMap();
                    });

                    services.AddRabbitServices(context.Configuration);
                    services.AddObjectSerializer();

                    services.AddOption<MovieWatcherOptions>(context.Configuration);
                    services.AddOption<SeriesWatcherOptions>(context.Configuration);

                    services.AddTransient<IClientFactory<RadarrClient>, RadarrClientFactory>();
                    services.AddTransient<IClientFactory<SonarrClient>, SonarrClientFactory>();

                    services.AddHostedService<MovieWatcher>();
                    services.AddHostedService<SeriesWatcher>();
                })
                .UseConsoleLifetime();
        }
    }
}
