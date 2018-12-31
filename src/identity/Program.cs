namespace identity
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4.EntityFramework.Mappers;
    using IdentityServer4.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NativeCode.Core.Data;
    using NativeCode.Core.Mvc;
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.WebHosting;
    using NativeCode.Node.Identity;
    using NativeCode.Node.Identity.Entities;
    using NativeCode.Node.Identity.SeedModels;
    using Serilog;

    public class Program
    {
        internal const string AppName = "Identity";

        internal const string Name = "Platform";

        internal const string Version = "v1";

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseKeyValueConfig(Name, AppName)
                .UseSerilog()
                .UseStartup<Startup>();
        }

        public static Task Main(string[] args)
        {
            return CreateWebHostBuilder(args)
                .ConfigureServices((context, options) => options.AddSerilog(context.Configuration, AppName))
                .Build()
                .Migrate<IdentityDataContext>("Development", "Production")
                .UseDataSeeder<IdentityDataContext>(Seed)
                .RunAsync();
        }

        private static async Task Seed(IDataContextSeeder<IdentityDataContext> seeder, IServiceScope scope)
        {
            var assembly = typeof(IdentityDataContext).Assembly;
            var hosting = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();
            var environment = hosting.EnvironmentName;

            await seeder.SeedAsync<ApiResource, IdentityServer4.EntityFramework.Entities.ApiResource>(
                assembly,
                $"NativeCode.Node.Identity.Seeding.{environment}.ApiResource.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Name == model.Name),
                (model, dbset) => Task.FromResult(model.ToEntity()));

            await seeder.SeedAsync<Client, IdentityServer4.EntityFramework.Entities.Client>(
                assembly,
                $"NativeCode.Node.Identity.Seeding.{environment}.Client.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.ClientId == model.ClientId),
                (model, dbset) => Task.FromResult(model.ToEntity()));

            await seeder.SeedAsync<IdentityResource, IdentityServer4.EntityFramework.Entities.IdentityResource>(
                assembly,
                "NativeCode.Node.Identity.Seeding.IdentityResource.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Name == model.Name),
                (model, dbset) => Task.FromResult(model.ToEntity()));

            await seeder.SaveChangesAsync();

            await seeder.SeedAsync<UserInfo, User>(
                assembly,
                $"NativeCode.Node.Identity.Seeding.{environment}.User.json",
                (model, dbset) => dbset.SingleOrDefaultAsync(x => x.Email == model.Email),
                async (model, dbset) =>
                {
                    using (var inner = scope.ServiceProvider.CreateScope())
                    using (var users = inner.ServiceProvider.GetRequiredService<UserManager<User>>())
                    {
                        var user = new User
                        {
                            Email = model.Email,
                            EmailConfirmed = model.EmailConfirmed,
                            UserName = model.UserName,
                        };

                        var result = await users.CreateAsync(user, model.Password);

                        if (result.Succeeded == false)
                        {
                            throw new AggregateException(result.Errors.Select(x => new InvalidOperationException(x.Description)));
                        }

                        return await dbset.FindAsync(user.Id);
                    }
                });
        }
    }
}
