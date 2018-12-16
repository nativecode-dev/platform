namespace identity
{
    using System.IO;
    using System.Threading.Tasks;
    using IdentityServer4.EntityFramework.Mappers;
    using IdentityServer4.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NativeCode.Core.Data;
    using NativeCode.Core.Web;
    using NativeCode.Node.Core;
    using NativeCode.Node.Identity;
    using NativeCode.Node.Identity.Entities;
    using NativeCode.Node.Identity.SeedModels;
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
                .MigrateDatabase<IdentityDataContext>("Development", "Production")
                .UseDataSeeder<IdentityDataContext>(Seed)
                .Run();
        }

        private static async Task Seed(IDataContextSeeder<IdentityDataContext> seeder, IServiceScope scope)
        {
            var assembly = typeof(IdentityDataContext).Assembly;

            using (var users = scope.ServiceProvider.GetRequiredService<UserManager<User>>())
            {
                await seeder.SeedAsync<ApiResource, IdentityServer4.EntityFramework.Entities.ApiResource>(assembly,
                    "NativeCode.Node.Identity.Seeding.ApiResource.json",
                    (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Name == model.Name),
                    model => Task.FromResult(model.ToEntity()));

                await seeder.SeedAsync<Client, IdentityServer4.EntityFramework.Entities.Client>(assembly,
                    "NativeCode.Node.Identity.Seeding.Client.json",
                    (model, dbset) => dbset.SingleOrDefaultAsync(x => x.ClientId == model.ClientId),
                    model => Task.FromResult(model.ToEntity()));

                await seeder.SeedAsync<IdentityResource, IdentityServer4.EntityFramework.Entities.IdentityResource>(assembly,
                    "NativeCode.Node.Identity.Seeding.IdentityResource.json",
                    (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Name == model.Name),
                    model => Task.FromResult(model.ToEntity()));

                await seeder.SaveChangesAsync();

                await seeder.SeedAsync<UserInfo, User>(assembly, "NativeCode.Node.Identity.Seeding.User.json",
                    (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Id == model.Id),
                    async model =>
                    {
                        var user = new User
                        {
                            Email = model.Email,
                            EmailConfirmed = model.EmailConfirmed,
                            UserName = model.UserName,
                        };

                        await users.CreateAsync(user, model.Password);

                        return user;
                    });
            }
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
