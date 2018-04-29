namespace NativeCode.Media.Data.Media
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Series : Entity<Guid>
    {
        public ICollection<MediaLink> Links { get; protected set; } = new Collection<MediaLink>();

        public ICollection<Season> Seasons { get; protected set; } = new Collection<Season>();

        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [MaxLength(DataTypeSize.Text.Normal)]
        public string Title { get; set; }
    }
}
