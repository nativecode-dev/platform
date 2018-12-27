namespace NativeCode.Node.Media.Data.Library
{
    using System;
    using NativeCode.Core.Data;

    public abstract class LibrarySource : Entity<Guid>
    {
        public string Description { get; set; }

        public string Name { get; set; }
    }
}
