namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Attributes;
    using NativeCode.Core.Data;

    [SecureEntity(nameof(Configuration))]
    public class Configuration : Entity<Guid>
    {
        [MaxLength(DataTypeSize.Text.Normal)]
        public string DefaultLocation { get; set; }

        public ConfigurationFormat Format { get; set; }

        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Schema { get; set; }

        public ICollection<ConfigurationTemplate> Templates { get; protected set; } = new Collection<ConfigurationTemplate>();
    }
}
