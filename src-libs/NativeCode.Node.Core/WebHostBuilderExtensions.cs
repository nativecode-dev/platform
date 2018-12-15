namespace NativeCode.Node.Core
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using NativeCode.Core;
    using NativeCode.Core.Configuration;

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseKeyValueConfig(this IWebHostBuilder host, string owner, string name)
        {
            return host.ConfigureAppConfiguration((context, builder) =>
            {
                var (global, common, env) = KeyValueServerConfig.Standard(owner, name, context.HostingEnvironment.EnvironmentName);

                builder.AddJsonFile("appsettings.json", false, true);
                builder.AddJsonFile($"appsettings.{env}.json", true, true);
                builder.AddEtcdConfig(global, common, env);
                builder.AddEnvironmentVariables();
            });
        }
    }
}
