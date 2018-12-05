namespace node
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using NativeCode.Core.Configuration;
    using Serilog;

    public class Program
    {
        internal const string ConfigRoot = "tcp://etcd:2379/NativeCode/Node/Node";

        internal const string ConfigShared = "tcp://etcd:2379/NativeCode/Shared";

        internal const string Name = "node";

        internal const string Version = "v1";

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
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
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
