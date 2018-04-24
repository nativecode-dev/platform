namespace NativeCode.Sync.SyncActions
{
    using System.Threading.Tasks;

    public interface ISyncAction
    {
        Task<ISyncActionResult> Sync<TSource, TTarget>(TSource source, TTarget target)
            where TSource : ISyncDescriptor where TTarget : ISyncDescriptor;
    }
}
