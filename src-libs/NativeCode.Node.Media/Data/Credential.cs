namespace NativeCode.Node.Media.Data
{
    using System;
    using NativeCode.Core.Data;

    public class Credential : Entity<Guid>
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public byte[] SshPrivateKey { get; set; }

        public byte[] SshPublicKey { get; set; }

        public CredentialType Type { get; set; }
    }
}
