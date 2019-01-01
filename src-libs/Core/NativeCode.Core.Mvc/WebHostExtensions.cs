namespace NativeCode.Core.Mvc
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Extensions;
    using Extensions;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Nito.AsyncEx;
    using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

    public static class WebHostExtensions
    {
        /// <summary>
        /// Performs migrations when before the request pipeline is setup.
        /// </summary>
        /// <remarks>
        /// NOTE: I know adding the migrations to the startup is NOT the recommended practice. However,
        /// until EFCore fixes their story for deploying migrations, we have to use the startup (or at least
        /// it's the least intrusive). The full "dotnet" tools are not available in a published add, ergo
        /// you cannot invoke "dotnet ef" because the EF tools are not deployed.
        /// <see href="https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-2.1&tabs=netcore-cli"/>
        /// <see href="https://github.com/aspnet/EntityFrameworkCore/issues/9033#issuecomment-317063370"/>
        /// <see href="https://github.com/dotnet/dotnet-docker-samples/issues/89"/>.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="host"></param>
        /// <param name="environments"></param>
        /// <returns></returns>
        public static IWebHost Migrate<T>([NotNull] this IWebHost host, params string[] environments)
            where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();
                var enabled = environments.Any() == false || environments.Any(name => env.IsEnvironment(name));
                var database = scope.ServiceProvider.GetRequiredService<T>();
                var migrations = database.Migrations();

                if (enabled && migrations.Any())
                {
                    database.Database.Migrate();
                }
            }

            return host;
        }

        public static IWebHost UseDataSeeder<T>(this IWebHost host, Func<IDataContextSeeder<T>, IServiceScope, Task> seed)
            where T : DbContext
        {
            return AsyncContext.Run(() => host.UseDataSeederAsync(seed));
        }

        public static async Task<IWebHost> UseDataSeederAsync<T>(this IWebHost host, Func<IDataContextSeeder<T>, IServiceScope, Task> seed)
            where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IDataContextSeeder<T>>();

                await seed(seeder, scope)
                    .NoCapture();

                return host;
            }
        }
    }
}
