namespace NativeCode.Core.Messaging.Options
{
    public class RabbitOptions
    {
        public bool DispatchConsumersAsync { get; set; } = true;

        public string Host { get; set; }

        public string Password { get; set; }

        public int Port { get; set; } = 5672;

        public string User { get; set; }

        public string VirtualHost { get; set; } = "/";
    }
}