namespace NativeCode.Node.Media.Models.Data
{
    using System;

    public abstract class DataModel
    {
        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }

        public Guid Id { get; set; }
    }
}
