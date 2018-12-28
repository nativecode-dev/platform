namespace NativeCode.Clients
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public interface IResourceRemover<in TLookupRequest>
    {
        Task<bool> Remove([NotNull] TLookupRequest request);
    }
}
