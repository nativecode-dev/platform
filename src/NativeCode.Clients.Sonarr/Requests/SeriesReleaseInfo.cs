namespace NativeCode.Clients.Sonarr.Requests
{
    public class SeriesReleaseInfo
    {
        public string DownloadUrl { get; set; }

        public string Title { get; set; }

        public Protocol Protocol { get; set; }

        public string PublishDate { get; set; }
    }
}
