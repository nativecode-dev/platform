namespace NativeCode.Sync.Data.Infrastructure.Domains
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class DomainCanonicalName : DomainRecord
    {
        [MaxLength(DataTypeSize.Name.Short)]
        public string HostName { get; set; }

        [MaxLength(DataTypeSize.Name.Normal)]
        public string Redirect { get; set; }
    }
}
