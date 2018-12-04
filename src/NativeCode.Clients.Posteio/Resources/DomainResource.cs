namespace NativeCode.Clients.Posteio.Resources
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Requests;
    using Responses;
    using RestSharp;

    public class DomainResource : ClientResource<Domain, CreateDomain, UpdateDomain>
    {
        private const string DomainQueryUrlFormat = "domains?query={0}&page={1}&paging={2}";

        private const string DomainsUrl = "domains";

        private const string DomainUrlFormat = "domains/{0}";

        internal DomainResource(RestClient client)
            : base(client)
        {
            this.DomainKeys = new DomainKeyResource(client);
        }

        public DomainKeyResource DomainKeys { get; }

        public Task<bool> Create([NotNull] CreateDomain domain)
        {
            return this.CreateResource(DomainsUrl, domain);
        }

        public Task<bool> Delete([NotNull] string domain)
        {
            return this.DeleteResource(string.Format(DomainUrlFormat, domain));
        }

        public Task<Domain> Get([NotNull] string domain)
        {
            return this.GetResource(string.Format(DomainUrlFormat, domain));
        }

        public Task<ResponsePage<Domain>> Query(string query = default(string), int page = 1, int paging = 50)
        {
            return this.QueryResource(string.Format(DomainQueryUrlFormat, query, page, paging));
        }

        public Task<bool> Update([NotNull] UpdateDomain domain)
        {
            return this.UpdateResource(string.Format(DomainUrlFormat, domain.Name), domain);
        }
    }
}
