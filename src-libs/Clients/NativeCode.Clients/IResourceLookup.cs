namespace NativeCode.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IResourceLookup<in TLookupRequest, TResponse>
    {
        Task<IEnumerable<TResponse>> All();

        Task<TResponse> GetResource(TLookupRequest request);
    }

    public interface IResourceLookup<TResponse>
    {
        Task<TResponse> GetResource();
    }
}
