namespace NativeCode.Clients.Radarr.Responses.Commands
{
    public class DownloadedMoviesScan : CommandOptions
    {
        public string DownloadClientId { get; set; }

        public ImportModeKind ImportMode { get; set; }

        public override CommandKind Command => CommandKind.DownloadedMoviesScan;

        public string Path { get; set; }
    }
}
