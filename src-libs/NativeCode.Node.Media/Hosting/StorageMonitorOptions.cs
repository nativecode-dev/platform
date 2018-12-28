namespace NativeCode.Node.Media.Hosting
{
    using System;
    using System.Collections.Generic;

    public class StorageMonitorOptions
    {
        public bool Enabled { get; set; }

        public IDictionary<string, string> Mounts { get; set; } = new Dictionary<string, string>();
    }
}
