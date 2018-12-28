namespace identity.Controllers.Status
{
    using System;

    public class StatusInfoViewModel
    {
        public string MachineName { get; set; } = Environment.MachineName;

        public AppOptions Options { get; set; }
    }
}
