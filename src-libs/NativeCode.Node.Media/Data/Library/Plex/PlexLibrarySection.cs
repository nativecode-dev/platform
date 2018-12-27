namespace NativeCode.Node.Media.Data.Library.Plex
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using NativeCode.Core.Data;

    public class PlexLibrarySection : Entity<Guid>
    {
        [ForeignKey(nameof(PlexServerInfoId))]
        public PlexServerInfo PlexServerInfo { get; set; }

        public Guid PlexServerInfoId { get; set; }

        public Guid SourceId { get; set; }

        public string Section { get; set; }
    }
}
