namespace NativeCode.Data.Commerce
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class Cart : Entity<Guid>, IHasAvailabilityDate
    {
        public DateTimeOffset? AvailabilityEnd { get; set; }

        public DateTimeOffset? AvailabilityStart { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }

        public Collection<CartItem> Items { get; protected set; } = new Collection<CartItem>();
    }
}
