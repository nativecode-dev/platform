namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using Responses;

    public class MissngMoviesSearchOptions : CommandOptions
    {
        public string FilterKey { get; set; }

        public string FilterValue { get; set; }

        public override CommandKind Name => CommandKind.CutOffUnmetMoviesSearch;
    }
}
