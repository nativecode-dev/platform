namespace NativeCode.Clients.Posteio.Resources
{
    using System.Globalization;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using NativeCode.Clients.Posteio.Requests;
    using NativeCode.Clients.Posteio.Responses;

    using RestSharp;

    public class DomainKeyResource : ClientResource<DomainKey, CreateDomainKey, CreateDomainKey>
    {
        private const string DomainKeyUrlFormat = "domains/{0}/dkim";

        internal DomainKeyResource(RestClient client)
            : base(client)
        {
        }

        public Task<bool> Delete([NotNull] string domain)
        {
            return this.DeleteResource(string.Format(CultureInfo.CurrentCulture, DomainKeyUrlFormat, domain));
        }

        public Task<DomainKey> Get([NotNull] string domain)
        {
            return this.GetResource(string.Format(CultureInfo.CurrentCulture, DomainKeyUrlFormat, domain));
        }

        public Task<bool> Regenerate([NotNull] DomainKey key)
        {
            return this.PutResource(string.Format(CultureInfo.CurrentCulture, DomainKeyUrlFormat, key.DomainName), key);
        }
    }
}
