namespace NativeCode.Node.Services
{
    public class IrcWatchOptions
    {
        public string Channel { get; set; }

        public string NickName { get; set; }

        public string RealName { get; set; }

        public string UserName { get; set; }

        public string Server { get; set; }

        public bool UseSsl { get; set; } = true;

        public string XspeedsSecret { get; set; } = "";
    }
}