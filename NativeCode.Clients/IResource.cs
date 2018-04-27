namespace NativeCode.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public interface IResource
    {
    }

    public interface IResource<in TRequest, TResponse> : IResource
    {
        Task<bool> Add([NotNull] TRequest request);

        Task<bool> Add([NotNull] IEnumerable<TRequest> requests);

        Task<IEnumerable<TResponse>> All();

        Task<IResourcePage<TResponse>> Page(int size, int start = 1);

        Task<IEnumerable<TResponse>> Query([NotNull] TRequest request);

        Task<IResourcePage<TResponse>> QueryPage([NotNull] TRequest request, int size, int start = 1);

        Task<bool> Remove([NotNull] TRequest request);

        Task<bool> Remove([NotNull] IEnumerable<TRequest> requests);
    }
}
