namespace node_delegate.Data
{
    using Microsoft.EntityFrameworkCore;
    using NativeCode.Core.Data;

    public class DelegateDataContext : DataContext
    {
        /// <inheritdoc />
        public DelegateDataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
