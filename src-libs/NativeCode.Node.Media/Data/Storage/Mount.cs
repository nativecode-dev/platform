namespace NativeCode.Node.Media.Data.Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using JetBrains.Annotations;
    using NativeCode.Core.Data;

    public class Mount : Entity<Guid>
    {
        [CanBeNull]
        [ForeignKey(nameof(CredentialId))]
        public Credential Credentials { get; set; }

        public Guid? CredentialId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Host { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public List<MountPath> Paths { get; } = new List<MountPath>();

        public MountType Type { get; set; }
    }
}
