namespace NativeCode.Node.Media.Data.Data.Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core.Enums;
    using JetBrains.Annotations;
    using NativeCode.Core.Data;

    public class Mount : Entity<Guid>
    {
        public Guid? CredentialId { get; set; }

        [CanBeNull]
        [ForeignKey(nameof(CredentialId))]
        public Credential Credentials { get; set; }

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
