namespace NativeCode.Sync.SyncActions.Posteio
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    public class SyncActionResult : ISyncActionResult
    {
        public SyncActionResult(SyncActionResultType status)
            : this(status, null, null)
        {
        }

        public SyncActionResult([NotNull] IEnumerable<string> errors)
            : this(SyncActionResultType.Failed, errors, null)
        {
        }

        public SyncActionResult(SyncActionResultType status, [CanBeNull] IEnumerable<string> errors, [CanBeNull] IEnumerable<string> messages)
        {
            this.Errors = errors ?? Enumerable.Empty<string>();
            this.Messages = messages ?? Enumerable.Empty<string>();
            this.Status = status;
        }

        public static ISyncActionResult None { get; } = new SyncActionResult(SyncActionResultType.None);

        public static ISyncActionResult Success { get; } = new SyncActionResult(SyncActionResultType.Success);

        public IEnumerable<string> Errors { get; }

        public IEnumerable<string> Messages { get; }

        public SyncActionResultType Status { get; }

        public static ISyncActionResult Failed([NotNull] IEnumerable<string> errors)
        {
            return new SyncActionResult(errors);
        }

        public static ISyncActionResult Partial(IEnumerable<string> messages)
        {
            return new SyncActionResult(SyncActionResultType.Partial, null, messages);
        }

        public static ISyncActionResult RecordSuccess(IEnumerable<string> messages)
        {
            return new SyncActionResult(SyncActionResultType.Success, null, messages);
        }
    }
}
