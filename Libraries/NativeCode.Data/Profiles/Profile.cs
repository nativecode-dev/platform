namespace NativeCode.Data.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Profile : Entity<Guid>
    {
        public List<ProfileContact> Contacts { get; } = new List<ProfileContact>();

        [Required]
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }
    }
}
