namespace NativeCode.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using NativeCode.Core.Data;

    public abstract class DataContext : DbContext
    {
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.SetDateModified();
            this.SetKeys();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.SetDateModified();
            this.SetKeys();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected virtual void SetDateModified()
        {
            var entities = this.ChangeTracker.Entries().Select(e => e.Entity).Cast<Entity>().Where(e => e.DateModified.HasValue == false);

            foreach (var entity in entities)
            {
                entity.DateModified = DateTimeOffset.UtcNow;
            }
        }

        protected virtual void SetKeys()
        {
            var requiresNewGuid = this.ChangeTracker.Entries().Select(e => e.Entity).OfType<Entity<Guid>>().Where(e => e.Key == Guid.Empty);

            foreach (var entity in requiresNewGuid)
            {
                entity.Key = Guid.NewGuid();
            }
        }
    }
}
