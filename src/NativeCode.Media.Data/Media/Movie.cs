namespace NativeCode.Media.Data.Media
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Movie : Entity<Guid>
    {
        [DataType(DataType.DateTime)]
        public DateTimeOffset? DateAired { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DateFetched { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DateReleased { get; set; }

        public ICollection<MediaFile> Files { get; protected set; } = new Collection<MediaFile>();

        public ICollection<MediaLink> Links { get; protected set; } = new Collection<MediaLink>();

        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [MaxLength(DataTypeSize.Text.Normal)]
        public string Title { get; set; }
    }
}
