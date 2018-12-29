namespace NativeCode.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IResourceQuery<in TQueryRequest, TResponse>
    {
        Task<IEnumerable<TResponse>> Find(TQueryRequest request);
    }
}
