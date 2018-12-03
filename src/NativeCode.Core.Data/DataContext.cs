namespace NativeCode.Core.Data
{
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.EntityFrameworkCore;

    public abstract class DataContext : DbContext
    {
        protected DataContext(DbContextOptions options)
            : base(options)
        {
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ValidateChanges(true);

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ValidateChanges(true);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableSoftDelete();

            base.OnModelCreating(modelBuilder);
        }
    }
}