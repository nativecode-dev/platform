namespace NativeCode.Node.Identity.JsonConverters
{
    using AutoMapper;
    using Core.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddIdentityConverters(this IServiceCollection services)
        {
            services.AddContextSeeder(
                provider =>
                {
                    var context = provider.GetRequiredService<IdentityDataContext>();
                    var logger = provider.GetRequiredService<ILogger<IDataContextSeeder<IdentityDataContext>>>();
                    var mapper = provider.GetRequiredService<IMapper>();
                    var resolver = provider.GetRequiredService<IdentityContractResolver>();
                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = resolver,
                    };

                    return new DataContextSeeder<IdentityDataContext>(context, settings, mapper, logger);
                });

            services.AddTransient<ClaimConverter>();
            services.AddTransient<PasswordConverter>();
            services.AddTransient<ScopeConverter>();
            services.AddTransient<SecretConverter>();
            services.AddTransient<IdentityContractResolver>();

            return services;
        }
    }
}
