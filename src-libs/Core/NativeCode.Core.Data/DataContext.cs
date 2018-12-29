namespace NativeCode.Core.Data
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.EntityFrameworkCore;

    public abstract class DataContext<T> : DbContext
        where T : DbContext
    {
        protected DataContext(DbContextOptions<T> options)
            : base(options)
        {
            this.Options = options.FindExtension<DataContextOptionsExtension>() ?? new DataContextOptionsExtension();
        }

        protected DataContextOptionsExtension Options { get; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            if (this.Options.EnableAuditing)
            {
                this.UpdateAuditEntities();
            }

            if (this.Options.EnableValidation)
            {
                this.ValidateChanges(true);
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        [SuppressMessage("ReSharper", "OptionalParameterHierarchyMismatch", Justification = "Reviewed.")]
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            if (this.Options.EnableAuditing)
            {
                this.UpdateAuditEntities();
            }

            if (this.Options.EnableValidation)
            {
                this.ValidateChanges(true);
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
