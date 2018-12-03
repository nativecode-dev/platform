namespace node_processor
{
    using System;
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NativeCode.Clients;
    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Radarr.Requests;
    using NativeCode.Clients.Sonarr;
    using NativeCode.Core.Configuration;
    using NativeCode.Core.Messaging.Extensions;
    using NativeCode.Core.Serialization;
    using NativeCode.Node.Messages;
    using NativeCode.Node.Services;

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
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName;
                    var defaultConfig = $"{ConfigRoot}/{Version}";
                    var environmentConfig = $"{ConfigRoot}/{env}";
                    var machineConfig = $"{ConfigRoot}/{Environment.MachineName}";

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
                    services.AddAutoMapper(config =>
                    {
                        config.CreateMap<MovieRelease, ReleaseInfo>()
                            .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Link))
                            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Protocol, opt => opt.MapFrom(src => Protocol.Torrent))
                            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.Now.ToLongDateString()))
                            .ReverseMap();

                        config.CreateMap<SeriesRelease, ReleaseInfo>()
                            .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Link))
                            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Protocol, opt => opt.MapFrom(src => Protocol.Torrent))
                            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.Now.ToLongDateString()))
                            .ReverseMap();
                    });

                    services.AddRabbitServices(context.Configuration);

                    services.AddTransient<IObjectSerializer, JsonObjectSerializer>();
                    services.AddTransient<IClientFactory<RadarrClient>, RadarrClientFactory>();
                    services.AddTransient<IClientFactory<SonarrClient>, SonarrClientFactory>();

                    services.Configure<MovieWatcherOptions>(context.Configuration.GetSection(nameof(MovieWatcherOptions)));
                    services.Configure<SeriesWatcherOptions>(context.Configuration.GetSection(nameof(SeriesWatcherOptions)));

                    services.AddHostedService<MovieWatcher>();
                    services.AddHostedService<SeriesWatcher>();
                })
                .UseConsoleLifetime();
        }
    }
}