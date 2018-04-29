namespace NativeCode.Core.Syncing
{
    public class SyncBuilderChange<TSource, TTarget>
    {
        public SyncBuilderChange(TSource source, TTarget target, SyncBuilderChangeType changeType)
        {
            this.ChangeType = changeType;
            this.Source = source;
            this.Target = target;
        }

        public SyncBuilderChangeType ChangeType { get; }

        public TSource Source { get; }

        public TTarget Target { get; }
    }
}
