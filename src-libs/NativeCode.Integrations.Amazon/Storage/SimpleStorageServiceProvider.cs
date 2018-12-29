namespace NativeCode.Integrations.Amazon.Storage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Storage;

    public class SimpleStorageServiceProvider : IRemoteFileStoreProvider
    {
        /// <inheritdoc />
        public string Scheme => "s3:";

        /// <inheritdoc />
        public bool CanAdapt(Uri location)
        {
            return location.Scheme == this.Scheme;
        }

        /// <inheritdoc />
        public Task<bool> CanConnect(Uri location)
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public RemoteFileStoreContext CreateContext(Uri location)
        {
            return new SimpleStorageServiceContext(location);
        }

        /// <inheritdoc />
        public Task<Stream> RetrieveAsync(RemoteFileStoreContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task StoreAsync(RemoteFileStoreContext context, Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
