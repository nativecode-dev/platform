namespace NativeCode.Data.Commerce
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Discount : Entity<Guid>, IHasAvailabilityDate
    {
        public DateTimeOffset? AvailabilityEnd { get; set; }

        public DateTimeOffset? AvailabilityStart { get; set; }

        [Range(0.0, 100.0)]
        public double Percentage { get; set; }
    }
}
