namespace NativeCode.Sync
{
    using Configuration;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureApp)
                .UseStartup<Startup>()
                .Build();
        }

        private static void ConfigureApp(WebHostBuilderContext context, IConfigurationBuilder config)
        {
            var env = context.HostingEnvironment.EnvironmentName;
            config.AddEtcdConfig(options => options.Host = $"http://etcd:2379/NativeCode/{env}/nsync");
        }

        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .Run();
        }
    }
}