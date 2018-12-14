namespace node
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using NativeCode.Node.Core;
    using Serilog;

    public class Program
    {
        internal const string AppName = "node";

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
                .UseKeyValueConfig(AppName, AppName)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
