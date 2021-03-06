namespace NativeCode.Node.Core.WebHosting
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using NativeCode.Core;
    using NativeCode.Core.Configuration;

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseKeyValueConfig(this IWebHostBuilder host, string owner, string name)
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
