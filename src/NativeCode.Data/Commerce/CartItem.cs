namespace NativeCode.Data.Commerce
{
    using System;

    using NativeCode.Core.Data;

    public class CartItem : Entity<Guid>
    {
        public ProductPrice Price { get; set; }

        public decimal Quantity { get; set; }

        public QuantityKind QuantityKind { get; set; }
    }
}
