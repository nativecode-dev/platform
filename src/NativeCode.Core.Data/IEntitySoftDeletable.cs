namespace NativeCode.Core.Data
{
    using System;

    public interface IEntitySoftDeletable
    {
        DateTimeOffset? DateSoftDeleted { get; set; }

        bool IsSoftDeleted { get; }
    }
}
