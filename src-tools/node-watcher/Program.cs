namespace node_watcher
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
        internal const string AppName = "Watcher";

        internal const string Name = "Platform";

        internal const string Version = "v1";

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder().ConfigureServices(
                    (context, services) =>
                        {
                            services.AddOption<NodeOptions>(context.Configuration, out var node);
                            services.AddOption<RabbitOptions>(context.Configuration, out var rabbit);
                            services.AddOption<IrcWatcherOptions>(context.Configuration, out var irc);
                            services.AddSerilog(context.Configuration, Name);

                            Log.Logger.Information(
                                "Startup: {@node}",
                                new
                                    {
                                        node.Name,
                                        node.RedisHost,
                                        RabbitHost = rabbit.Host,
                                        RabbitUser = rabbit.User,
                                        IrcHost = irc.Host,
                                        IrcUser = irc.UserName,
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

        private static Task Main(string[] args)
        {
            return CreateHostBuilder(args)
                .Build()
                .RunAsync();
        }
    }
}
