namespace node_delegate
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.WebHosting;
    using Serilog;

    public class Program
    {
        internal const string AppName = "Node";

        internal const string Name = "Platform";

        internal const string Version = "v1";

        public static Task Main(string[] args)
        {
            return CreateWebHostBuilder(args)
                .ConfigureServices((context, options) => options.AddSerilog(context.Configuration, AppName))
                .Build()
                .RunAsync();
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
