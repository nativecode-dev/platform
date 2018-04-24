namespace NativeCode.Data.Profiles
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class ProfileIdentity : Entity<Guid>
    {
        [MaxLength(DataTypeSize.Name.Normal)]
        public string DisplayName { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(DataTypeSize.Email)]
        public string Email { get; set; }

        [MaxLength(DataTypeSize.Name.Short)]
        public string FirstName { get; set; }

        [MaxLength(DataTypeSize.Name.Short)]
        public string LastName { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(DataTypeSize.Url)]
        public string WebAddress { get; set; }
    }
}
