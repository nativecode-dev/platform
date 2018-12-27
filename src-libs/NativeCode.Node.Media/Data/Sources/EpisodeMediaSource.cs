namespace NativeCode.Node.Media.Data.Sources
{
    using System;
    using Series;

    public class EpisodeMediaSource : MediaSource
    {
        public Episode Episode { get; set; }

        public Guid EpisodeId { get; set; }
    }
}