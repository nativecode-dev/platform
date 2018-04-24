namespace NativeCode.Sync.Commands
{
    public abstract class SyncCommand
    {
        public string Source { get; set; }

        public string Target { get; set; }
    }
}