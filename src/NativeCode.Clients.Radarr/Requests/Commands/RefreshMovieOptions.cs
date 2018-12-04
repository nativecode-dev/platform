namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using Responses;

    public class RefreshMovieOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.RefreshMovie;

        public int? MovieId { get; set; }
    }
}
