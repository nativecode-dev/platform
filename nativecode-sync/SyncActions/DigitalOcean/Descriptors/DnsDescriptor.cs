namespace NativeCode.Sync.SyncActions.DigitalOcean.Descriptors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    public class DnsDescriptor : DigitalOceanDescriptor
    {
        public DnsDescriptor(IDistributedCache cache, string token)
            : base(cache, token, Descriptor.DigitalOcean.Dns)
        {
        }

        public override Task CreateKey(string key, ISyncDescriptor source)
        {
            return Task.CompletedTask;
        }

        public override Task RemoveKey(string key, ISyncDescriptor source)
        {
            return Task.CompletedTask;
        }

        protected override async Task<IReadOnlyCollection<string>> DescriptorKeys()
        {
            var domains = await this.Client.Domains.GetAll();

            return domains.Select(d => d.Name).ToList();
        }

        protected override async Task<object> DescriptorValue(string key, string name)
        {
            var domain = await this.Client.Domains.Get(key);
            return this.GetPropertyValue(name, domain);
        }
    }
}
