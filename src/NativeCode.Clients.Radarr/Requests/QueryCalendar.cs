namespace NativeCode.Clients.Radarr.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class QueryCalendar
    {
        [DataType(DataType.DateTime)]
        public DateTimeOffset? End { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? Start { get; set; }
    }
}