namespace NativeCode.Sync.SyncActions.Posteio.Descriptors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    using NativeCode.Posteio.Requests;
    using NativeCode.Posteio.Responses;

    public class DomainDescriptor : PosteioDescriptor
    {
        public DomainDescriptor(IDistributedCache cache, string userAgent, string hostname, string username, string password)
            : base(cache, userAgent, hostname, username, password)
        {
        }

        public override Task CreateKey(string key, ISyncDescriptor source)
        {
            var domain = new CreateDomain
            {
                DomainName = key,
                ForwardDomain = "nativecode.com",
                Forward = true
            };

            return this.Client.Domains.Create(domain);
        }

        public override Task RemoveKey(string key, ISyncDescriptor source)
        {
            return this.Client.Domains.Delete(key);
        }

        protected override async Task<IReadOnlyCollection<string>> DescriptorKeys()
        {
            var results = new List<Domain>();

            var domains = await this.Client.Domains.Query();
            results.AddRange(domains.Results);

            while (domains.Page < domains.LastPage)
            {
                domains = await this.Client.Domains.Query(null, domains.Page + 1, domains.Paging);
                results.AddRange(domains.Results);
            }

            return results.Select(r => r.Name).ToList();
        }

        protected override async Task<object> DescriptorValue(string key, string name)
        {
            var domain = await this.Client.Domains.Get(key);
            return this.GetPropertyValue(name, domain);
        }
    }
}
