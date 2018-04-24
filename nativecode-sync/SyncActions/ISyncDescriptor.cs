namespace NativeCode.Sync.SyncActions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISyncDescriptor
    {
        string UserAgent { get; }

        Task CreateKey(string key, ISyncDescriptor source);

        Task<IReadOnlyCollection<string>> GetKeys();

        Task<object> GetKeyValue(string name, string key);

        Task RemoveKey(string key, ISyncDescriptor source);
    }
}
