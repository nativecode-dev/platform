namespace NativeCode.Data.Commerce
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class ProductPrice : Entity<Guid>, IHasAvailabilityDate
    {
        public DateTimeOffset? AvailabilityEnd { get; set; }

        public DateTimeOffset? AvailabilityStart { get; set; }

        public decimal Cost { get; set; }

        [Range(0.0, 100.0)]
        public double Markup { get; set; } = 10.0;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public SubscriptionKind SubscriptionKind { get; set; }
    }
}
