namespace NativeCode.Core.Data.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class MigrationExtensions
    {
        /// <summary>
        /// Determines whether the <see cref="DbContext"/> has migrations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool HasMigrations<T>([NotNull] this T context)
            where T : DbContext
        {
            return context.Migrations()
                .Any();
        }

        /// <summary>
        /// Performs migrations when before the request pipeline is setup.
        /// </summary>
        /// <remarks>
        /// NOTE: I know adding the migrations to the startup is NOT the recommended practice. However,
        /// until EFCore fixes their story for deploying migrations, we have to use the startup (or at least
        /// it's the least intrusive). The full "dotnet" tools are not available in a published add, ergo
        /// you cannot invoke "dotnet ef" because the EF tools are not deployed. - MP
        /// <see cref="https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-2.1&tabs=netcore-cli"/>
        /// <see cref="https://github.com/aspnet/EntityFrameworkCore/issues/9033#issuecomment-317063370"/>
        /// <see cref="https://github.com/dotnet/dotnet-docker-samples/issues/89"/>.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="host"></param>
        /// <param name="environments"></param>
        /// <returns></returns>
        public static IHost MigrateDatabase<T>([NotNull] this IHost host, params string[] environments)
            where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();
                var enabled = environments.Any(name => env.IsEnvironment(name));
                var database = scope.ServiceProvider.GetRequiredService<T>();
                var migrations = database.Migrations();

                if (enabled && migrations.Any())
                {
                    database.Database.Migrate();
                }
            }

            return host;
        }

        /// <summary>
        /// Migrate the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost MigrateDatabase<T>([NotNull] this IHost host)
            where T : DbContext
        {
            return host.MigrateDatabase<T>("Development", "Production");
        }

        /// <summary>
        /// Gets the available migrations to run.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IEnumerable<string> Migrations<T>([NotNull] this T context)
            where T : DbContext
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(migration => migration.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations.Select(migration => migration.Key);

            return total.Except(applied);
        }
    }
}
