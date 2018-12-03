namespace NativeCode.Core.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddEtcdConfig(this IConfigurationBuilder builder,
            Action<EtcdOptions> configure)
        {
            var options = new EtcdOptions();
            configure(options);

            builder.Sources.Add(new EtcdConfigurationSource(options));

            return builder;
        }

        public static IConfigurationBuilder AddEtcdConfig(this IConfigurationBuilder builder, params string[] hosts)
        {
            foreach (var host in hosts)
            {
                builder.AddEtcdConfig(options => options.Host = host);
            }

            return builder;
        }
    }
}