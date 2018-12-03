namespace NativeCode.Clients.Radarr.Responses
{
    using System;

    public class CommandInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool SendUpdatesToClient { get; set; }

        public DateTimeOffset StartedOn { get; set; }

        public string State { get; set; }

        public DateTimeOffset StateChangeTime { get; set; }
    }
}