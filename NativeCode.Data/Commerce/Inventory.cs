namespace NativeCode.Data.Commerce
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class Inventory : Entity<Guid>
    {
    }

    public class InventoryStatus : Entity<Guid>
    {
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}
