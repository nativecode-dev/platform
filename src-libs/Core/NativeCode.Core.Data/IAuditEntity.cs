namespace NativeCode.Core.Data
{
    using System;

    public interface IAuditEntity
    {
        DateTimeOffset DateCreated { get; set; }

        DateTimeOffset? DateModified { get; set; }
    }
}
