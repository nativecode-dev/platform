namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using NativeCode.Core.Attributes;
    using NativeCode.Core.Data;

    [SecureEntity(nameof(ConfigurationGroup))]
    public class ConfigurationGroup : Entity<Guid>
    {
        public ICollection<Configuration> Configurations { get; protected set; } = new Collection<Configuration>();
    }
}
