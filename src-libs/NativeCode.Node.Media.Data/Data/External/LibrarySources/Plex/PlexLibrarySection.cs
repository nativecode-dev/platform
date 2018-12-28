namespace NativeCode.Node.Media.Data.Data.External.LibrarySources.Plex
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class PlexLibrarySection : Entity<Guid>
    {
        [ForeignKey(nameof(PlexServerInfoId))]
        public PlexServerInfo PlexServerInfo { get; set; }

        public Guid PlexServerInfoId { get; set; }

        [Required]
        public string Section { get; set; }

        public Guid SourceId { get; set; }
    }
}
