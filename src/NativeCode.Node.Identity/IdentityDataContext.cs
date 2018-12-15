namespace NativeCode.Node.Identity
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using IdentityServer4.EntityFramework.Entities;
    using IdentityServer4.EntityFramework.Interfaces;
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using UserClaim = Entities.UserClaim;

    public class IdentityDataContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IConfigurationDbContext
    {
        public IdentityDataContext(ConfigurationStoreOptions store, DbContextOptions<IdentityDataContext> options)
            : base(options)
        {
            this.StoreOptions = store;
        }

        protected ConfigurationStoreOptions StoreOptions { get; }

        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return this.SaveChangesAsync(true, CancellationToken.None);
        }
    }
}
