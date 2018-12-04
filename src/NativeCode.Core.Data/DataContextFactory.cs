namespace NativeCode.Core.Data
{
    using System;
    using System.IO;
    using Core.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public abstract class DataContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        protected string AspNetCoreEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        protected string ContextName => typeof(TContext).ToShortKey();

        public TContext CreateDbContext(string[] args)
        {
            return this.Create(Directory.GetCurrentDirectory(), this.AspNetCoreEnvironment);
        }

        protected virtual TContext Create()
        {
            return this.Create(Directory.GetCurrentDirectory(), this.AspNetCoreEnvironment);
        }

        protected virtual TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"{this.ContextName} is null or empty.", nameof(connectionString));
            }

            var builder = new DbContextOptionsBuilder<TContext>();

            return this.CreateNewInstance(builder, connectionString);
        }

        protected virtual TContext Create(string basePath, string environment)
        {
            var builder = this.Configure(basePath, environment, new ConfigurationBuilder());

            var config = builder.Build();

            var connectionString = config.GetConnectionString(this.ContextName);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Could not find a connection string named '{this.ContextName}'.");
            }

            return this.Create(connectionString);
        }

        protected virtual IConfigurationBuilder Configure(string basePath, string environment,
            IConfigurationBuilder builder)
        {
            return builder.SetBasePath(basePath)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables();
        }

        protected abstract TContext CreateNewInstance(DbContextOptionsBuilder<TContext> builder,
            string connectionString);
    }
}
