namespace NativeCode.Sync.SyncActions
{
    using System.Threading.Tasks;

    using NativeCode.Sync.SyncActions.Posteio;

    public abstract class SyncAction : ISyncAction
    {
        public Task<ISyncActionResult> Sync<TSource, TTarget>(TSource source, TTarget target)
            where TSource : ISyncDescriptor where TTarget : ISyncDescriptor
        {
            if (this.CanExecute(source, target))
            {
                return this.PerformSync(source, target);
            }

            return Task.FromResult(SyncActionResult.None);
        }

        protected abstract bool CanExecute<TSource, TTarget>(TSource source, TTarget target)
            where TSource : ISyncDescriptor where TTarget : ISyncDescriptor;

        protected abstract Task<ISyncActionResult> PerformSync<TSource, TTarget>(TSource source, TTarget target)
            where TSource : ISyncDescriptor where TTarget : ISyncDescriptor;
    }
}
