namespace NativeCode.Sync.SyncActions
{
    using System.Collections.Generic;

    public interface ISyncActionResult
    {
        IEnumerable<string> Errors { get; }

        IEnumerable<string> Messages { get; }

        SyncActionResultType Status { get; }
    }
}
