namespace NativeCode.Node.Media.Data.External.LibrarySources.Plex
{
    using System.Collections.Generic;

    public class PlexLibrarySource : LibrarySource
    {
        public List<PlexServerInfo> PlexServerInfo { get; } = new List<PlexServerInfo>();
    }
}
