namespace node_scheduler
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.WebHosting;
    using Serilog;

    public class Program
    {
        internal const string AppName = "Scheduler";

        internal const string Name = "Platform";

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
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseKeyValueConfig(Name, AppName)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
