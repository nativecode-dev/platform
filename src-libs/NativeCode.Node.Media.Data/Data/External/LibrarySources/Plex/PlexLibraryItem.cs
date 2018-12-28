namespace NativeCode.Node.Media.Data.Data.External.LibrarySources.Plex
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;
    using NativeCode.Node.Media.Core.Enums;

    public class PlexLibraryItem : Entity<Guid>
    {
        [ForeignKey(nameof(PlexLibrarySectionId))]
        public PlexLibrarySection PlexLibrarySection { get; set; }

        public Guid PlexLibrarySectionId { get; set; }

        public PlexMediaItemType PlexMediaItemType { get; set; }
    }
}
