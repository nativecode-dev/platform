namespace NativeCode.Sync.Data.Infrastructure.Providers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class ProviderProperty : Entity<Guid>
    {
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }

        public string Value { get; set; }

        [MaxLength(DataTypeSize.Name.Normal)]
        public string ValueType { get; set; }
    }
}
