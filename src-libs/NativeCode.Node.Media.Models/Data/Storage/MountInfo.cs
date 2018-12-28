namespace NativeCode.Node.Media.Models.Data.Storage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using JetBrains.Annotations;

    using NativeCode.Node.Media.Core.Enums;

    public class MountInfo : DataModel
    {
        [CanBeNull]
        public CredentialInfo Credentials { get; set; }

        [Required]
        [MaxLength(256)]
        public string Host { get; set; }

        public MountType MountType { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public List<MountPathInfo> Paths { get; } = new List<MountPathInfo>();
    }
}
