namespace NativeCode.Node.Media.Data.Data.Catalog.Shows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Season : MediaInfo
    {
        public List<Episode> Episodes { get; } = new List<Episode>();

        [ForeignKey(nameof(SeriesId))]

        public Series Series { get; set; }

        public Guid SeriesId { get; set; }
    }
}
