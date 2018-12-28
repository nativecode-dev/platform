namespace NativeCode.Node.Media.Data.Data.MediaSources
{
    using System;

    using NativeCode.Node.Media.Data.Data.Catalog.Movies;

    public class MediaSourceMovie : MediaSource
    {
        public Movie Movie { get; set; }

        public Guid MovieId { get; set; }
    }
}
