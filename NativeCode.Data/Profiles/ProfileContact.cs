namespace NativeCode.Data.Profiles
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class ProfileContact : Entity<Guid>
    {
        public ProfileIdentity Identity { get; set; }

        public ProfileLocation Location { get; set; }

        [MaxLength(DataTypeSize.Phone)]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(ProfileId))]
        public Profile Profile { get; set; }

        public Guid ProfileId { get; set; }
    }
}
