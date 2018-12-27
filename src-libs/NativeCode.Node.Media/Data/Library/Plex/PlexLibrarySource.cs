namespace NativeCode.Node.Media.Data.Library.Plex
{
    using System.Collections.Generic;

    public class PlexLibrarySource : LibrarySource
    {
        public List<PlexServerInfo> PlexServerInfo { get; set; } = new List<PlexServerInfo>();
    }
}