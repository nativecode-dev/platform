namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using NativeCode.Clients.Radarr.Responses;

    public class RefreshMovieOptions : CommandOptions
    {
        public int? MovieId { get; set; }

        public override CommandKind Name => CommandKind.RefreshMovie;
    }
}
