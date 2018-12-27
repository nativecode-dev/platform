namespace NativeCode.Node.Media.Data.Movies
{
    using System.Collections.Generic;

    public class MovieCollection : MediaInfo
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}