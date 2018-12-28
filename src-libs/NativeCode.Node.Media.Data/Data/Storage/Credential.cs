namespace NativeCode.Node.Media.Data.Data.Storage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Core;
    using Core.Enums;
    using NativeCode.Core.Data;

    public class Credential : Entity<Guid>
    {
        public string Description { get; set; }

        [MaxLength(256)]
        public string Login { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Password { get; set; }

        public byte[] SshPrivateKey { get; set; }

        public byte[] SshPublicKey { get; set; }

        public CredentialType Type { get; set; }
    }
}
