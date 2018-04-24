namespace NativeCode.Sync.SyncActions.Posteio.Descriptors
{
    using Microsoft.Extensions.Caching.Distributed;

    using NativeCode.Posteio;

    public abstract class PosteioDescriptor : SyncDescriptor
    {
        protected PosteioDescriptor(IDistributedCache cache, string userAgent, string hostname, string username, string password)
            : base(cache, userAgent)
        {
            this.Client = new PosteioClient(hostname, username, password);
        }

        protected IPosteioClient Client { get; }
    }
}
