namespace NativeCode.Node.Media.Data.MediaSources
{
    using System;
    using System.Collections.Generic;
    using NativeCode.Core.Data;
    using Storage;

    public abstract class MediaSource : Entity<Guid>
    {
        public List<MountPathFile> Files { get; } = new List<MountPathFile>();
    }
}
