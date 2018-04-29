namespace NativeCode.Clients
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public interface IResourceCreator<in TCreateRequest>
    {
        Task<bool> Add([NotNull] TCreateRequest request);
    }
}