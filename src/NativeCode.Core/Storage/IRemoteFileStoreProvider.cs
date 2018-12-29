namespace NativeCode.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    public interface IRemoteFileStoreProvider
    {
        string Scheme { get; }

        bool CanAdapt([NotNull] Uri location);

        Task<bool> CanConnect([NotNull] Uri location);

        RemoteFileStoreContext CreateContext(Uri location);

        RemoteFileStoreContext CreateContext(RemoteFileStoreContext context);

        [NotNull]
        Task<Stream> RetrieveAsync([NotNull] RemoteFileStoreContext context, CancellationToken cancellationToken);

        Task StoreAsync([NotNull] RemoteFileStoreContext context, [NotNull] Stream stream, CancellationToken cancellationToken);
    }
}
