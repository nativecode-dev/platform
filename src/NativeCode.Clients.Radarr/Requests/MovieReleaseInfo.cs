namespace NativeCode.Clients.Radarr.Requests
{
    public class MovieReleaseInfo
    {
        public string DownloadUrl { get; set; }

        public string Title { get; set; }

        public Protocol Protocol { get; set; }

        public string PublishDate { get; set; }
    }
}
