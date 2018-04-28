namespace NativeCode.Clients.Radarr.Responses.Commands
{
    public class RefreshMovieOptions : CommandOptions
    {
        public override CommandKind Command => CommandKind.RefreshMovie;

        public int? MovieId { get; set; }
    }
}
