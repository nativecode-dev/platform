namespace identity
{
    using System.IO;
    using System.Threading.Tasks;
    using IdentityServer4.EntityFramework.Mappers;
    using IdentityServer4.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using NativeCode.Core.Data;
    using NativeCode.Core.Web;
    using NativeCode.Node.Core;
    using NativeCode.Node.Identity;
    using NativeCode.Node.Identity.Entities;
    using Serilog;

    public class Program
    {
        internal const string AppName = "Identity";

        internal const string Name = "Platform";

        internal const string Version = "v1";

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .MigrateDatabase<IdentityDataContext>()
                .UseDataSeeder<IdentityDataContext>(Seed)
                .Run();
        }

        private static Task Seed(IDataContextSeeder<IdentityDataContext> seeder)
        {
            var assembly = typeof(IdentityDataContext).Assembly;

            seeder.SeedAsync<ApiResource, IdentityServer4.EntityFramework.Entities.ApiResource>(assembly,
                "NativeCode.Node.Identity.Seeding.ApiResource.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Name == model.Name),
                model => model.ToEntity());

            seeder.SeedAsync<Client, IdentityServer4.EntityFramework.Entities.Client>(assembly,
                "NativeCode.Node.Identity.Seeding.Client.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.ClientId != model.ClientId),
                model => model.ToEntity());

            seeder.SeedAsync<IdentityResource, IdentityServer4.EntityFramework.Entities.IdentityResource>(assembly,
                "NativeCode.Node.Identity.Seeding.IdentityResource.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Name == model.Name),
                model => model.ToEntity());

            seeder.SeedAsync<User, User>(assembly, "NativeCode.Node.Identity.Seeding.User.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Id == model.Id),
                model => model);

            return Task.CompletedTask;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseKeyValueConfig(Name, AppName)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
