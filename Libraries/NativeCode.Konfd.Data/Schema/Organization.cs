namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Attributes;
    using NativeCode.Core.Data;
    using NativeCode.Konfd.Data.Security;

    [SecureEntity(nameof(Organization))]
    public class Organization : Entity<Guid>
    {
        public ICollection<Application> Applications { get; protected set; } = new Collection<Application>();

        public ICollection<Domain> Domains { get; protected set; } = new Collection<Domain>();

        public ICollection<SecurityKey> Keys { get; protected set; } = new Collection<SecurityKey>();

        [Required]
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }

        public ICollection<Tag> Tags { get; protected set; } = new Collection<Tag>();
    }
}
