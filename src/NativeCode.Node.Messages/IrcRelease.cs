namespace NativeCode.Node.Messages
{
    using Core.Messaging;

    public class IrcRelease : QueueMessage
    {
        public string Name { get; set; }

        public string Size { get; set; }

        public string Category { get; set; }

        public string Uploader { get; set; }

        public string Link { get; set; }
    }
}
