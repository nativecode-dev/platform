namespace NativeCode.Node.Core
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using NativeCode.Core.Configuration;

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseKeyValueConfig(this IWebHostBuilder host, string name, string applicationName)
        {
            return host.ConfigureAppConfiguration((context, builder) =>
            {
                var env = context.HostingEnvironment.EnvironmentName;

                var common = $"tcp://etcd:2379/root/{name}/Common";
                var options = $"tcp://etcd:2379/root/{name}/{applicationName}/{env}";

                builder.AddJsonFile("appsettings.json", false, true);
                builder.AddJsonFile($"appsettings.{env}.json", true, true);
                builder.AddEtcdConfig(common, options);
                builder.AddEnvironmentVariables();
            });
        }
    }
}
