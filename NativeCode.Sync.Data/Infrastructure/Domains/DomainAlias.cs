namespace NativeCode.Sync.Data.Infrastructure.Domains
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class DomainAlias : DomainRecord
    {
        [MaxLength(DataTypeSize.Name.Short)]
        public string HostName { get; set; }
    }
}
