namespace NativeCode.Node.Media.Data.Storage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
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

        [SuppressMessage("Microsoft.Performance", "CA1819")]
        public byte[] SshPrivateKey { get; set; }

        [SuppressMessage("Microsoft.Performance", "CA1819")]
        public byte[] SshPublicKey { get; set; }

        public CredentialType Type { get; set; }
    }
}
