namespace NativeCode.Media.Data.Media
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Season : Entity<Guid>
    {
        public ICollection<Episode> Episodes { get; protected set; } = new Collection<Episode>();

        public ICollection<MediaLink> Links { get; protected set; } = new Collection<MediaLink>();

        public int Number { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [MaxLength(DataTypeSize.Text.Normal)]
        public string Title { get; set; }
    }
}
