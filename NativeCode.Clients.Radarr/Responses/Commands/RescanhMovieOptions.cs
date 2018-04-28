namespace NativeCode.Clients.Radarr.Responses.Commands
{
    public class RescanhMovieOptions : CommandOptions
    {
        public override CommandKind Command => CommandKind.RescanMovie;

        public int? MovieId { get; set; }
    }
}
