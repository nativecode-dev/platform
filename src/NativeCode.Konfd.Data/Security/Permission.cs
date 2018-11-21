namespace NativeCode.Konfd.Data.Security
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Permission : Entity<Guid>
    {
        public PermissionFlags Flags { get; set; }

        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }
    }
}
