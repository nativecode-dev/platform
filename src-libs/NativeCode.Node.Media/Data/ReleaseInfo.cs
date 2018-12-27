namespace NativeCode.Node.Media.Data
{
    using System;
    using NativeCode.Core.Data;

    public class ReleaseInfo : Entity<Guid>
    {
        public DateTimeOffset? AnnounceDate { get; set; }

        public DateTimeOffset? ReleaseDate { get; set; }

        public DateTimeOffset? PublishDate { get; set; }

        public DateTimeOffset? StreamDate { get; set; }
    }
}
