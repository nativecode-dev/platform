namespace NativeCode.Node.Media.Hosting
{
    using System.Collections.Generic;

    public class MediaDiskMonitorOptions
    {
        public bool Enabled { get; set; }

        public IList<MediaDiskMonitorMountPathOption> MountPaths { get; set; } = new List<MediaDiskMonitorMountPathOption>();
    }
}
