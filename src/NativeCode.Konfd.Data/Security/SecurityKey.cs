namespace NativeCode.Konfd.Data.Security
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class SecurityKey : Entity<Guid>
    {
        [DataType(DataType.DateTime)]
        public DateTimeOffset? Expiration { get; set; }

        [Required]
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }

        public bool Revoked { get; set; }

        [Required]
        [MaxLength(DataTypeSize.Text.Normal)]
        public string SecretKey { get; set; }

        [Required]
        [MaxLength(DataTypeSize.Text.Normal)]
        public string SharedKey { get; set; }
    }
}
