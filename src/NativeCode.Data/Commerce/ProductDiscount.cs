namespace NativeCode.Data.Commerce
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class ProductDiscount : Entity<Guid>
    {
        [ForeignKey(nameof(DiscountId))]
        public Discount Discount { get; set; }

        public Guid DiscountId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}
