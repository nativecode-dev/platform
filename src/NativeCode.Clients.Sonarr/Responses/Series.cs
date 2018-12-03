namespace NativeCode.Clients.Sonarr.Responses
{
    using System;
    using System.Collections.Generic;

    public class Series
    {
        public string AirTime { get; set; }

        public string CleanTitle { get; set; }

        public DateTimeOffset? FirstAired { get; set; }

        public int Id { get; set; }

        public IEnumerable<SeriesImage> Images { get; set; }

        public string ImdbId { get; set; }

        public DateTimeOffset LastInfoSync { get; set; }

        public bool Monitored { get; set; }

        public string Network { get; set; }

        public string Overview { get; set; }

        public string Path { get; set; }

        public int QualityProfileId { get; set; }

        public int Runtime { get; set; }

        public bool SeasonFolder { get; set; }

        public string SeriesType { get; set; }

        public string Status { get; set; }

        public string Title { get; set; }

        public string TitleSlug { get; set; }

        public int TvdbId { get; set; }

        public int TvRageId { get; set; }

        public bool UseSceneNumbering { get; set; }

        public int Year { get; set; }
    }
}