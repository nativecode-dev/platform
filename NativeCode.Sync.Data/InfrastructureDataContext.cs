namespace NativeCode.Sync.Data
{
    using Microsoft.EntityFrameworkCore;

    using NativeCode.Data;
    using NativeCode.Sync.Data.Infrastructure;
    using NativeCode.Sync.Data.Infrastructure.Domains;

    public class InfrastructureDataContext : DataContext
    {
        public DbSet<Domain> Domains { get; protected set; }
    }
}
