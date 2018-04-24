namespace NativeCode.Sync.Data.Infrastructure.Domains
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Domain : Entity<Guid>
    {
        public Collection<DomainAlias> Aliases { get; protected set; } = new Collection<DomainAlias>();

        public Collection<DomainAliasIpv6> AliasesIpv6 { get; protected set; } = new Collection<DomainAliasIpv6>();

        public Collection<DomainCanonicalName> CanonicalNames { get; protected set; } = new Collection<DomainCanonicalName>();

        public Collection<DomainCertificateAuthority> CertificateAuthorities { get; protected set; } = new Collection<DomainCertificateAuthority>();

        [DefaultValue(true)]
        public bool Enabled { get; set; } = true;

        public Collection<DomainMailExchange> MailExchanges { get; protected set; } = new Collection<DomainMailExchange>();

        [MaxLength(DataTypeSize.Domain)]
        public string Name { get; set; }

        public Collection<DomainService> Services { get; protected set; } = new Collection<DomainService>();
    }
}
