namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Attributes;
    using NativeCode.Core.Data;

    [SecureEntity(nameof(Application))]
    public class Application : Entity<Guid>
    {
        [Required]
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }

        public ICollection<Tag> Tags { get; protected set; } = new Collection<Tag>();

        public ICollection<ApplicationVersion> Versions { get; protected set; } = new Collection<ApplicationVersion>();
    }
}
