namespace NativeCode.Sync.SyncActions.Posteio
{
    using System.Threading.Tasks;

    public class SyncMailDomainKeys : SyncAction
    {
        protected override bool CanExecute<TSource, TTarget>(TSource source, TTarget target)
        {
            return source.UserAgent == Descriptor.Posteio.Domain && target.UserAgent == Descriptor.DigitalOcean.Dns;
        }

        protected override Task<ISyncActionResult> PerformSync<TSource, TTarget>(TSource source, TTarget target)
        {
            return Task.FromResult(SyncActionResult.None);
        }
    }
}
