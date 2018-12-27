namespace NativeCode.Node.Media.Data.Series
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Episode : MediaInfoMetadata
    {
        [ForeignKey(nameof(SeasonId))]
        public Season Season { get; set; }

        public Guid SeasonId { get; set; }

        [ForeignKey(nameof(SeriesId))]
        public Series Series { get; set; }

        public Guid SeriesId { get; set; }

        [ForeignKey(nameof(ReleaseInfoId))]
        public ReleaseInfo ReleaseInfo { get; set; }

        public Guid? ReleaseInfoId { get; set; }
    }
}
