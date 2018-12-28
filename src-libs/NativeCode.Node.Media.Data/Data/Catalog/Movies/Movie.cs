namespace NativeCode.Node.Media.Data.Data.Catalog.Movies
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using JetBrains.Annotations;

    public class Movie : MediaMetadata
    {
        [CanBeNull]
        [ForeignKey(nameof(MovieCollectionId))]
        public MovieCollection MovieCollection { get; set; }

        public Guid? MovieCollectionId { get; set; }

        [ForeignKey(nameof(ReleaseInfoId))]
        public ReleaseInfo ReleaseInfo { get; set; }

        public Guid? ReleaseInfoId { get; set; }
    }
}
