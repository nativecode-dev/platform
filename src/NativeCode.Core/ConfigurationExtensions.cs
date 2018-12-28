namespace NativeCode.Core
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NativeCode.Core.Aws;
    using NativeCode.Core.Options;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddAws(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAwsCredentialProvider, AwsOptionsCredentialProvider>();
            services.Configure<AwsOptions>(configuration);

            return services;
        }
    }
}
