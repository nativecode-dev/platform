namespace NativeCode.Node.Media.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Core;
    using Core.Enums;
    using JetBrains.Annotations;

    public class MountInfo
    {
        [CanBeNull]
        public CredentialInfo Credentials { get; set; }

        [Required]
        [MaxLength(256)]
        public string Host { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public List<MountPathInfo> Paths { get; } = new List<MountPathInfo>();

        public MountType MountType { get; set; }
    }
}
