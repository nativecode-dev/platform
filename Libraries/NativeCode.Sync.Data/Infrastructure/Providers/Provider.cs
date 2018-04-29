namespace NativeCode.Sync.Data.Infrastructure.Providers
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Provider : Entity<Guid>
    {
        [DefaultValue(true)]
        public bool Enabled { get; set; } = true;

        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }

        public Collection<ProviderProperty> Properties { get; protected set; } = new Collection<ProviderProperty>();

        public Collection<ProviderSecret> Secrets { get; protected set; } = new Collection<ProviderSecret>();

        public Collection<ProviderService> Services { get; protected set; } = new Collection<ProviderService>();
    }
}
