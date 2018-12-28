namespace NativeCode.Node.Media.Models
{
    using System.ComponentModel.DataAnnotations;
    using Core;
    using Core.Enums;

    public class CredentialInfo
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
