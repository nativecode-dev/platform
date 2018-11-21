namespace NativeCode.Data.Commerce
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public interface IHasAvailabilityDate
    {
        [DataType(DataType.DateTime)]
        DateTimeOffset? AvailabilityEnd { get; }

        [DataType(DataType.DateTime)]
        DateTimeOffset? AvailabilityStart { get; }
    }
}
