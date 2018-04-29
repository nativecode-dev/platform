namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using NativeCode.Clients.Radarr.Responses;

    public class RescanhMovieOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.RescanMovie;

        public int? MovieId { get; set; }
    }
}
