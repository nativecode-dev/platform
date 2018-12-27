namespace NativeCode.Node.Media.Data
{
    using System;
    using System.Collections.Generic;
    using NativeCode.Core.Data;
    using Sources;
    using Storage;

    public abstract class MediaSource : Entity<Guid>
    {
        public List<MountPathFile> MediaFiles { get; set; } = new List<MountPathFile>();
    }
}