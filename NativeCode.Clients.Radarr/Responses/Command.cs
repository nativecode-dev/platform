namespace NativeCode.Clients.Radarr.Responses
{
    using System;

    public class Command
    {
        public CommandBody Body { get; set; }

        public int Id { get; set; }

        public bool Manual { get; set; }

        public string Name { get; set; }

        public string Priority { get; set; }

        public DateTimeOffset Queued { get; set; }

        public bool SendUpdatesToClient { get; set; }

        public DateTimeOffset StartedOn { get; set; }

        public string State { get; set; }

        public string Status { get; set; }

        public string Trigger { get; set; }

        public bool UpdateScheduledTask { get; set; }
    }
}