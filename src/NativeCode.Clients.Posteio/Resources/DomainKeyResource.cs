namespace NativeCode.Clients.Posteio.Resources
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Requests;
    using Responses;
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
            return this.DeleteResource(string.Format(DomainKeyUrlFormat, domain));
        }

        public Task<DomainKey> Get([NotNull] string domain)
        {
            return this.GetResource(string.Format(DomainKeyUrlFormat, domain));
        }

        public Task<bool> Regenerate([NotNull] DomainKey key)
        {
            return this.PutResource(string.Format(DomainKeyUrlFormat, key.DomainName), key);
        }
    }
}