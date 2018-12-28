namespace NativeCode.Node.Identity
{
    using IdentityServer4.EntityFramework.Options;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using NativeCode.Core;
    using NativeCode.Core.Configuration;
    using NativeCode.Core.Data;

    public class IdentityDataContextFactory : DataContextFactory<IdentityDataContext>
    {
        private const string AppName = "Platform";

        protected override IConfigurationBuilder Configure(string basePath, string environment, IConfigurationBuilder builder)
        {
            var name = typeof(IdentityDataContext).Name.Replace("DataContext", string.Empty);
            var configs = KeyValueServerConfig.Standard(AppName, name, environment);

            return base.Configure(basePath, environment, builder)
                .AddEtcdConfig(configs);
        }

        protected override IdentityDataContext CreateNewInstance(
            DbContextOptionsBuilder<IdentityDataContext> builder,
            string connectionString)
        {
            builder.UseSqlServer(connectionString);

            var store = new ConfigurationStoreOptions();

            return new IdentityDataContext(store, builder.Options);
        }
    }
}
