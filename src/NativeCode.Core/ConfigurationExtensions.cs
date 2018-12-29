namespace NativeCode.Core
{
    using Aws;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Options;

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
