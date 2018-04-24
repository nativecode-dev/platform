namespace NativeCode.Sync.Data.Infrastructure.Domains
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class DomainMailExchange : DomainRecord
    {
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Handler { get; set; }

        [MaxLength(DataTypeSize.Name.Short)]
        public string HostName { get; set; }

        public int Priority { get; set; }
    }
}
