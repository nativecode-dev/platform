namespace NativeCode.Node.Media
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using NativeCode.Core;
    using NativeCode.Core.Configuration;
    using NativeCode.Core.Data;

    public class MediaDataContextFactory : DataContextFactory<MediaDataContext>
    {
        private const string AppName = "Platform";

        protected override IConfigurationBuilder Configure(string basePath, string environment, IConfigurationBuilder builder)
        {
            var name = typeof(MediaDataContext).Name.Replace("DataContext", string.Empty);
            var configs = KeyValueServerConfig.Standard(AppName, name, environment);

            return base.Configure(basePath, environment, builder)
                .AddEtcdConfig(configs);
        }

        protected override MediaDataContext CreateNewInstance(DbContextOptionsBuilder<MediaDataContext> builder,
            string connectionString)
        {
            builder.UseSqlServer(connectionString);

            return new MediaDataContext(builder.Options);
        }
    }
}
