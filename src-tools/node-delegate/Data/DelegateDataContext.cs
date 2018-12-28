namespace node_delegate.Data
{
    using Microsoft.EntityFrameworkCore;

    using NativeCode.Core.Data;

    public class DelegateDataContext : DataContext<DelegateDataContext>
    {
        /// <inheritdoc />
        public DelegateDataContext(DbContextOptions<DelegateDataContext> options)
            : base(options)
        {
        }
    }
}
