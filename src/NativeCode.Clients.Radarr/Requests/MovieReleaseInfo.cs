namespace NativeCode.Clients.Radarr.Requests
{
    using System.Diagnostics.CodeAnalysis;

    public class MovieReleaseInfo
    {
        [SuppressMessage("Microsoft.Design", "CA1056")]
        public string DownloadUrl { get; set; }

        public string Title { get; set; }

        public string Protocol { get; set; }

        public string PublishDate { get; set; }
    }
}
