namespace NativeCode.Node.Identity
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Data.Extensions;
    using Entities;
    using IdentityServer4.EntityFramework.Entities;
    using IdentityServer4.EntityFramework.Extensions;
    using IdentityServer4.EntityFramework.Interfaces;
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using UserClaim = Entities.UserClaim;

    public class IdentityDataContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IConfigurationDbContext, IDataProtectionKeyContext
    {
        public IdentityDataContext(ConfigurationStoreOptions store, DbContextOptions<IdentityDataContext> options)
            : base(options)
        {
            this.StoreOptions = store;
        }

        protected ConfigurationStoreOptions StoreOptions { get; }

        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return this.SaveChangesAsync(true, CancellationToken.None);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureClientContext(this.StoreOptions);
            builder.ConfigureResourcesContext(this.StoreOptions);

            builder.SeedJsonDataFromManifest<Role>("NativeCode.Node.Identity.Seeding.Role.json");
            builder.SeedJsonDataFromManifest<Role>("NativeCode.Node.Identity.Seeding.RoleClaim.json");

            base.OnModelCreating(builder);
        }
    }
}
