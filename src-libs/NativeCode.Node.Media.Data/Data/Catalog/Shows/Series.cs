namespace NativeCode.Node.Media.Data.Data.Catalog.Shows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Series : MediaInfo
    {
        public List<Season> Seasons { get; } = new List<Season>();

        [ForeignKey(nameof(ReleaseInfoId))]
        public ReleaseInfo ReleaseInfo { get; set; }

        public Guid? ReleaseInfoId { get; set; }
    }
}
