namespace NativeCode.Node.Media.Data
{
    using System;
    using NativeCode.Core.Data;

    public class Tag : Entity<Guid>
    {
        public string Name { get; set; }

        public string Normalized { get; set; }
    }
}
