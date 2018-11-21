namespace NativeCode.Konfd.Data.Security
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using NativeCode.Core.Data;

    public class Resource : Entity<Guid>
    {
        public ICollection<Permission> Permissions { get; protected set; } = new Collection<Permission>();
    }
}
