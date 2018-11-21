namespace NativeCode.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddEtcdConfig(this IConfigurationBuilder builder, Action<EtcdOptions> configure)
        {
            var options = new EtcdOptions();
            configure(options);

            builder.Sources.Add(new EtcdConfigurationSource(options));

            return builder;
        }
    }
}