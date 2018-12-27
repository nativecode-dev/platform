namespace NativeCode.Node.Media.Data.Sources
{
    using System;
    using Movies;

    public class MovieMediaSource : MediaSource
    {
        public Movie Movie { get; set; }

        public Guid MovieId { get; set; }
    }
}