namespace NativeCode.Core.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RemoteFileStore : IRemoteFileStore
    {
        public RemoteFileStore(IEnumerable<IRemoteFileStoreProvider> adapters)
        {
            this.Adapters = adapters;
        }

        protected IEnumerable<IRemoteFileStoreProvider> Adapters { get; }

        /// <inheritdoc />
        public async Task<RemoteFileStoreAdapter> From(Uri location)
        {
            var adapter = this.Adapters.FirstOrDefault(a => a.CanAdapt(location));

            if (adapter == null)
            {
                throw new InvalidOperationException($"Failed to find adapter for: {location}");
            }

            var connected = await adapter.CanConnect(location)
                .ConfigureAwait(false);

            if (connected == false)
            {
                throw new InvalidOperationException($"Failed to connect adapter to: {location}");
            }

            return new RemoteFileStoreAdapter(adapter, location);
        }
    }
}
