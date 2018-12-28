namespace node
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using NativeCode.Core.Web;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.WebHosting;
    using NativeCode.Node.Media;
    using Serilog;

    public class Program
    {
        internal const string AppName = "Node";

        internal const string Name = "Platform";

        internal const string Version = "v1";

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .ConfigureServices((context, options) => options.AddSerilog(context.Configuration, AppName))
                .Build()
                .MigrateDatabase<MediaDataContext>("Development")
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
