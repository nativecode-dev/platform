namespace NativeCode.Node.Services
{
    public class IrcWatchOptions
    {
        public string Server { get; set; }

        public string UserName { get; set; }

        public bool UseSsl { get; set; }

        public string XspeedsSecret { get; set; } = "";
    }
}
