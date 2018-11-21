namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Attributes;
    using NativeCode.Core.Data;

    [SecureEntity(nameof(Domain))]
    public class Domain : Entity<Guid>
    {
        [DefaultValue(true)]
        public bool Active { get; set; } = true;

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DateConfirmed { get; set; }

        [MaxLength(DataTypeSize.Name.Normal)]
        public string DomainName { get; set; }

        [MaxLength(DataTypeSize.TopLevelDomain)]
        public string DomainTld { get; set; }

        [MaxLength(DataTypeSize.Name.Tiny)]
        public string ValidationName { get; set; }

        public DomainValidationType ValidationType { get; set; }

        [MaxLength(DataTypeSize.Name.Normal)]
        public string ValidationValue { get; set; }
    }
}
