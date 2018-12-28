namespace NativeCode.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public interface IStorageService : IDisposable
    {
        Task<Stream> RetrieveStream(string bucket, string key, Stream stream);

        Task StoreStream(string bucket, string key, Stream stream);
    }
}
