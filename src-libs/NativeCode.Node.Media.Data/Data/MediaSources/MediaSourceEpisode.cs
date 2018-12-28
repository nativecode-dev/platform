namespace NativeCode.Node.Media.Data.Data.MediaSources
{
    using System;

    using NativeCode.Node.Media.Data.Data.Catalog.Shows;

    public class MediaSourceEpisode : MediaSource
    {
        public Episode Episode { get; set; }

        public Guid EpisodeId { get; set; }
    }
}
