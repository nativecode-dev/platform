namespace NativeCode.Clients.Radarr.Responses
{
    public class CommandBody
    {
        public string CompletionMessage { get; set; }

        public string Name { get; set; }

        public bool SendUpdatesToClient { get; set; }

        public string Trigger { get; set; }

        public bool UpdateScheduledTask { get; set; }
    }
}
