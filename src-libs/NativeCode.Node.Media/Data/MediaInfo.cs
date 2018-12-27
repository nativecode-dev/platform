namespace NativeCode.Node.Media.Data
{
    using System;
    using System.Collections.Generic;
    using NativeCode.Core.Data;

    public abstract class MediaInfo : Entity<Guid>
    {
        public string Description { get; set; }

        public string SortTitle { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public string Title { get; set; }
    }
}
