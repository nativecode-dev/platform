namespace NativeCode.Node.Media.Data.Data.Catalog.Movies
{
    using System.Collections.Generic;

    public class MovieCollection : MediaInfo
    {
        public List<Movie> Movies { get; } = new List<Movie>();
    }
}
