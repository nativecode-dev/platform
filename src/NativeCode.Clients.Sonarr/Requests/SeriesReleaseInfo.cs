namespace NativeCode.Clients.Sonarr.Requests
{
    using System.Diagnostics.CodeAnalysis;

    public class SeriesReleaseInfo
    {
        [SuppressMessage("Microsoft.Design", "CA1056")]
        public string DownloadUrl { get; set; }

        public string Title { get; set; }

        public string Protocol { get; set; }

        public string PublishDate { get; set; }
    }
}
