namespace NativeCode.Konfd.Data.Security
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Role : Entity<Guid>
    {
        public ICollection<SecurityKey> Keys { get; protected set; } = new Collection<SecurityKey>();

        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }

        public ICollection<Permission> Permissions { get; protected set; } = new Collection<Permission>();
    }
}
