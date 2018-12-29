namespace node_processor
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NativeCode.Clients;
    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Sonarr;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Messaging.Extensions;
    using NativeCode.Core.Messaging.Options;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.Hosting;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Services;
    using NativeCode.Node.Services.Watchers;
    using Serilog;

    internal class Program
    {
        internal const string AppName = "Processor";

        internal const string Name = "Platform";

        internal const string Version = "v1";

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder().ConfigureServices(
                    (context, services) =>
                    {
                        services.AddOption<NodeOptions>(context.Configuration, out var node);
                        services.AddOption<RabbitOptions>(context.Configuration, out var rabbit);
                        services.AddOption<MovieWatcherOptions>(context.Configuration, out var movies);
                        services.AddOption<SeriesWatcherOptions>(context.Configuration, out var series);
                        services.AddSerilog(context.Configuration, Name);

                        Log.Logger.Information(
                            "Startup: {@node}",
                            new
                            {
                                node.Name,
                                node.RedisHost,
                                RabbitHost = rabbit.Host,
                                RabbitUser = rabbit.User,
                                MoviesEndpoint = movies.Endpoint,
                                SeriesEndpoint = series.Endpoint,
                            });

                        services.AddDistributedRedisCache(
                            options =>
                            {
                                options.Configuration = node.RedisHost;
                                options.InstanceName = AppName;
                            });
                    })
                .ConfigureServices(
                    (context, services) =>
                    {
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
                .UseConsoleLifetime()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment(Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT"))
                .UseKeyValueConfig(Name, AppName)
                .UseSerilog();
        }

        private static Task Main(string[] args)
        {
            return CreateHostBuilder(args)
                .Build()
                .RunAsync();
        }
    }
}
