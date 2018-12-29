namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using System.Collections.Generic;
    using Responses;

    public class MoviesSearchOptions : CommandOptions
    {
        public IEnumerable<int> MovieIds { get; set; }

        public override CommandKind Name => CommandKind.MoviesSearch;
    }
}
