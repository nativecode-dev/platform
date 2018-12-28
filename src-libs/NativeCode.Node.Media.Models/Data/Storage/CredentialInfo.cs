namespace NativeCode.Node.Media.Models.Data.Storage
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Node.Media.Core.Enums;

    public class CredentialInfo : DataModel
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
