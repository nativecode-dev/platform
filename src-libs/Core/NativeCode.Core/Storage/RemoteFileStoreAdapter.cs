namespace NativeCode.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;

    public class RemoteFileStoreAdapter
    {
        public RemoteFileStoreAdapter(IRemoteFileStoreProvider provider, Uri location)
        {
            this.Location = location;
            this.Provider = provider;
        }

        protected Uri Location { get; }

        protected IRemoteFileStoreProvider Provider { get; }

        public async Task<Stream> RetrieveAsync(string filepath, CancellationToken cancellationToken)
        {
            var context = this.Provider.CreateContext(this.Location);
            context.FilePath = filepath;

            var stream = await this.Provider.RetrieveAsync(context, cancellationToken)
                .NoCapture();

            return stream;
        }

        public async Task StoreAsync(string filepath, Stream stream, CancellationToken cancellationToken)
        {
            var context = this.Provider.CreateContext(this.Location);
            context.FilePath = filepath;

            await this.Provider.StoreAsync(context, stream, cancellationToken)
                .NoCapture();
        }
    }
}
