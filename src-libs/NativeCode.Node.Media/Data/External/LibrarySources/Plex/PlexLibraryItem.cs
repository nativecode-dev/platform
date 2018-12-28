namespace NativeCode.Node.Media.Data.External.LibrarySources.Plex
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using NativeCode.Core.Data;

    public class PlexLibraryItem : Entity<Guid>
    {
        [ForeignKey(nameof(PlexLibrarySectionId))]
        public PlexLibrarySection PlexLibrarySection { get; set; }

        public Guid PlexLibrarySectionId { get; set; }

        public PlexMediaItemType PlexMediaItemType { get; set; }
    }
}
