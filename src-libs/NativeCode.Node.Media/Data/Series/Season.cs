namespace NativeCode.Node.Media.Data.Series
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Season : MediaInfo
    {
        public List<Episode> Episodes { get; set; } = new List<Episode>();

        [ForeignKey(nameof(SeriesId))]

        public Series Series { get; set; }

        public Guid SeriesId { get; set; }
    }
}
