namespace NativeCode.Clients.Radarr.Requests
{
    using System;

    public class QueryCalendar
    {
        public DateTimeOffset? End { get; set; }

        public DateTimeOffset? Start { get; set; }
    }
}
