namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using Responses;

    public class RefreshMovieOptions : CommandOptions
    {
        public int? MovieId { get; set; }

        public override CommandKind Name => CommandKind.RefreshMovie;
    }
}
