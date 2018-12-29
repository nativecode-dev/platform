namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using Responses;

    public class RescanhMovieOptions : CommandOptions
    {
        public int? MovieId { get; set; }

        public override CommandKind Name => CommandKind.RescanMovie;
    }
}
