namespace NativeCode.Sync.Data.Infrastructure.Domains
{
    public enum DomainRecordKind
    {
        Default = 0,

        Alias = Default,

        AliasIpv6,

        CanonicalName,

        CertificateAuthority,

        MailExchange,

        Service
    }
}
