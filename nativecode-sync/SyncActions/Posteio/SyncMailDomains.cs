namespace NativeCode.Sync.SyncActions.Posteio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NativeCode.Core.Syncing;

    public class SyncMailDomains : SyncAction
    {
        protected override bool CanExecute<TSource, TTarget>(TSource source, TTarget target)
        {
            return source.UserAgent == Descriptor.DigitalOcean.Dns && target.UserAgent == Descriptor.Posteio.Domain;
        }

        protected override async Task<ISyncActionResult> PerformSync<TSource, TTarget>(TSource source, TTarget target)
        {
            var nsdomains = await source.GetKeys();
            var mxdomains = await target.GetKeys();

            var sync = new SyncBuilder<string, string>(nsdomains, mxdomains);
            var changes = sync.Diff();

            var errors = new List<string>();

            foreach (var change in changes)
            {
                try
                {
                    switch (change.ChangeType)
                    {
                        case SyncBuilderChangeType.Add:
                            await target.CreateKey(change.Source, source);
                            break;

                        case SyncBuilderChangeType.Remove:
                            await target.RemoveKey(change.Source, source);
                            break;

                        default: continue;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"{{ source:{change.Source}, target:{change.Target}, error: {ex.Message} }}");
                }
            }

            if (errors.Any())
            {
                return SyncActionResult.Failed(errors);
            }

            return SyncActionResult.Success;
        }
    }
}
