namespace NativeCode.Data.Commerce
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class StatementLine : Entity<Guid>
    {
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Range(0.0, 100.0)]
        public double ProductDiscount { get; set; }

        public Guid ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ProductQuantity { get; set; }

        public QuantityKind ProductQuantityKind { get; set; }
    }
}
