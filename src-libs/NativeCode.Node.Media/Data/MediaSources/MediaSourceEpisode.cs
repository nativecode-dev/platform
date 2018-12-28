namespace NativeCode.Node.Media.Data.MediaSources
{
    using System;
    using Catalog.Series;

    public class MediaSourceEpisode : MediaSource
    {
        public Episode Episode { get; set; }

        public Guid EpisodeId { get; set; }
    }
}
