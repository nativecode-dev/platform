namespace NativeCode.Konfd.Data
{
    using Microsoft.EntityFrameworkCore;

    using NativeCode.Data;
    using NativeCode.Konfd.Data.Schema;
    using NativeCode.Konfd.Data.Security;

    public class KonfigDataContext : DataContext
    {
        public DbSet<Application> Applications { get; protected set; }

        public DbSet<Domain> Domains { get; protected set; }

        public DbSet<Organization> Organizations { get; protected set; }

        public DbSet<Permission> Permissions { get; protected set; }

        public DbSet<Resource> Resources { get; protected set; }

        public DbSet<Role> Roles { get; protected set; }

        public DbSet<Tag> Tags { get; protected set; }
    }
}
