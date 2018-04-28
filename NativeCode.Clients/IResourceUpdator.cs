namespace NativeCode.Clients
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public interface IResourceUpdator<in TUpdateRequest>
    {
        Task<bool> Update([NotNull] TUpdateRequest request);
    }
}