namespace NativeCode.Sync.Data.Infrastructure.Providers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class ProviderService : Entity<Guid>
    {
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }
    }
}