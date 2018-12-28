namespace identity.Controllers.Status
{
    using System;

    public class StatusInfoViewModel
    {
        public AppOptions Options { get; set; }

        public string MachineName { get; set; } = Environment.MachineName;
    }
}
