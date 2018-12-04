namespace NativeCode.Clients.Radarr.Responses
{
    using System;
    using System.Collections.Generic;

    public class Movie
    {
        public DateTimeOffset Added { get; set; }

        public IEnumerable<AlternativeTitle> AlternativeTitles { get; set; }

        public string CleanTitle { get; set; }

        public bool Downloaded { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public bool HasFile { get; set; }

        public int Id { get; set; }

        public IEnumerable<MovieImage> Images { get; set; }

        public string ImdbId { get; set; }

        public DateTimeOffset InCinemas { get; set; }

        public DateTimeOffset LastInfoSync { get; set; }

        public string MinimumAvailability { get; set; }

        public bool Monitor { get; set; }

        public string Overview { get; set; }

        public string Path { get; set; }

        public int ProfileId { get; set; }

        public int QualityProfileId { get; set; }

        public MovieRating Ratings { get; set; }

        public int Runtime { get; set; }

        public long SizeOnDisk { get; set; }

        public string SortTitle { get; set; }

        // NOTE: Potential enum.
        public string Status { get; set; }

        public string Studio { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public string Title { get; set; }

        public string TitleSlug { get; set; }

        public string TmdbId { get; set; }

        public string Website { get; set; }

        public int Year { get; set; }

        public string YouTubeTrailcerId { get; set; }
    }
}
