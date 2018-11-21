namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using NativeCode.Clients.Radarr.Responses;

    public class RefreshMovieOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.RefreshMovie;

        public int? MovieId { get; set; }
    }
}
