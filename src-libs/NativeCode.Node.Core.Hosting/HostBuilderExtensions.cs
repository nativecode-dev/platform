namespace NativeCode.Node.Core.Hosting
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    using NativeCode.Core;
    using NativeCode.Core.Configuration;

    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseKeyValueConfig(this IHostBuilder host, string owner, string name)
        {
            return host.ConfigureAppConfiguration(
                (context, builder) =>
                    {
                        var environment = context.HostingEnvironment.EnvironmentName;
                        var configs = KeyValueServerConfig.Standard(owner, name, environment);

                        builder.AddJsonFile("appsettings.json", false, true);
                        builder.AddJsonFile($"appsettings.{environment}.json", true, true);
                        builder.AddEtcdConfig(configs);
                        builder.AddEnvironmentVariables();
                    });
        }
    }
}
