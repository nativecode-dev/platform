namespace NativeCode.Sync.Data.Infrastructure.Domains
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public abstract class DomainRecord : Entity<Guid>
    {
        [ForeignKey(nameof(DomainId))]
        public Domain Domain { get; set; }

        public int DomainId { get; set; }

        public DomainRecordKind RecordType { get; set; }

        [DefaultValue(3600)]
        public int TimeToLive { get; set; }
    }
}
