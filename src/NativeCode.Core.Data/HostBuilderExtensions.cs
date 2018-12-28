namespace NativeCode.Core.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Nito.AsyncEx;

    public static class HostBuilderExtensions
    {
        public static IHost UseDataSeeder<T>(this IHost host, Func<IDataContextSeeder<T>, Task> seed)
            where T : DbContext
        {
            return AsyncContext.Run(() => UseDataSeederAsync(host, seed));
        }

        public static async Task<IHost> UseDataSeederAsync<T>(this IHost host, Func<IDataContextSeeder<T>, Task> seed)
            where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IDataContextSeeder<T>>();

                await seed(seeder)
                    .ConfigureAwait(false);

                return host;
            }
        }
    }
}
