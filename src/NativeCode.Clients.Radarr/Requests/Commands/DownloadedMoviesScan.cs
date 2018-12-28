namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using NativeCode.Clients.Radarr.Responses;

    public class DownloadedMoviesScan : CommandOptions
    {
        public string DownloadClientId { get; set; }

        public ImportModeKind ImportMode { get; set; }

        public override CommandKind Name => CommandKind.DownloadedMoviesScan;

        public string Path { get; set; }
    }
}
