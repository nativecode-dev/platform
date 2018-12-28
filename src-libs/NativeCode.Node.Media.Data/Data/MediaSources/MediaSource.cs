namespace NativeCode.Node.Media.Data.Data.MediaSources
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.Data;
    using NativeCode.Node.Media.Data.Data.Storage;

    public abstract class MediaSource : Entity<Guid>
    {
        public List<MountPathFile> Files { get; } = new List<MountPathFile>();
    }
}
