namespace NativeCode.Core.Storage
{
    using System;
    using System.Threading.Tasks;

    public interface IRemoteFileStore
    {
        Task<RemoteFileStoreAdapter> From(Uri location);
    }
}
