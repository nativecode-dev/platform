namespace NativeCode.Data
{
    using Microsoft.EntityFrameworkCore;

    using NativeCode.Data.Commerce;

    public class CommerceDataContext : DataContext
    {
        public DbSet<Cart> Carts { get; protected set; }

        public DbSet<Customer> Customers { get; protected set; }

        public DbSet<Discount> Discounts { get; protected set; }

        public DbSet<Inventory> Inventory { get; protected set; }

        public DbSet<ProductDiscount> ProductDiscounts { get; protected set; }

        public DbSet<Product> Products { get; protected set; }

        public DbSet<Statement> Statements { get; protected set; }
    }
}
