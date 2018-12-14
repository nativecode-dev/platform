namespace node
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using NativeCode.Core.Configuration;
    using Serilog;

    public class Program
    {
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

                    var common = "tcp://etcd:2379/root/Platform/Common";
                    var options = $"tcp://etcd:2379/root/Platform/Node/{env}";

                    builder.AddJsonFile("appsettings.json", false, true);
                    builder.AddJsonFile($"appsettings.{env}.json", true, true);
                    builder.AddEtcdConfig(common, options);
                    builder.AddEnvironmentVariables();
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
