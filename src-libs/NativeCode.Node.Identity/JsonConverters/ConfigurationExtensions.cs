namespace NativeCode.Node.Identity.JsonConverters
{
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddIdentityConverters(this IServiceCollection services)
        {
            services.AddTransient<ClaimConverter>();
            services.AddTransient<ScopeConverter>();
            services.AddTransient<SecretConverter>();
            services.AddTransient<IdentityContractResolver>();

            services.AddSingleton(provider =>
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = provider.GetRequiredService<IdentityContractResolver>(),
                };

                return settings;
            });

            return services;
        }
    }
}
