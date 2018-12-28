namespace NativeCode.Node.Media.Data.Data.Catalog.Shows
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Episode : MediaMetadata
    {
        [ForeignKey(nameof(SeasonId))]
        public Season Season { get; set; }

        public Guid SeasonId { get; set; }

        [ForeignKey(nameof(ReleaseInfoId))]
        public ReleaseInfo ReleaseInfo { get; set; }

        public Guid? ReleaseInfoId { get; set; }
    }
}
