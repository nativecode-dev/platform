namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class ApplicationVersion : Entity<Guid>
    {
        public Application Application { get; set; }

        public ICollection<Configuration> Configurations { get; protected set; } = new Collection<Configuration>();

        [Required]
        [MaxLength(DataTypeSize.Name.Tiny)]
        public string Version { get; set; }
    }
}