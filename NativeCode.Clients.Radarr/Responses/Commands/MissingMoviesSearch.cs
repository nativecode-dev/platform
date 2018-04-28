namespace NativeCode.Clients.Radarr.Responses.Commands
{
    public class MissngMoviesSearchOptinos : CommandOptions
    {
        public string FilterKey { get; set; }

        public string FilterValue { get; set; }

        public override CommandKind Command => CommandKind.CutOffUnmetMoviesSearch;
    }
}
