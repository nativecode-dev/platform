namespace NativeCode.Node.Services.Watchers
{
    public class IrcWatcherOptions
    {
        public string Host { get; set; }

        public string UserName { get; set; }

        public bool UseSsl { get; set; }

        public string XspeedsSecret { get; set; } = string.Empty;
    }
}
