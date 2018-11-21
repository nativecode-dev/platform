namespace NativeCode.Data.Commerce
{
    using System;
    using System.Collections.ObjectModel;

    using NativeCode.Core.Data;

    public class Product : Entity<Guid>, IHasAvailabilityDate
    {
        public DateTimeOffset? AvailabilityEnd { get; set; }

        public DateTimeOffset? AvailabilityStart { get; set; }

        public Collection<ProductDiscount> Discounts { get; protected set; } = new Collection<ProductDiscount>();

        public Collection<ProductPrice> Prices { get; protected set; } = new Collection<ProductPrice>();

        public TaxabilityStatus Taxability { get; set; }
    }
}
