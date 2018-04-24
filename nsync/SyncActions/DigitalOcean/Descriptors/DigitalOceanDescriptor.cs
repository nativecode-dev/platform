namespace NativeCode.Sync.SyncActions.DigitalOcean.Descriptors
{
    using global::DigitalOcean.API;

    using Microsoft.Extensions.Caching.Distributed;

    public abstract class DigitalOceanDescriptor : SyncDescriptor
    {
        protected DigitalOceanDescriptor(IDistributedCache cache, string token, string userAgent)
            : base(cache, userAgent)
        {
            this.Client = new DigitalOceanClient(token);
        }

        protected IDigitalOceanClient Client { get; }
    }
}
