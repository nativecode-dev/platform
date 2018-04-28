namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using System.Collections.Generic;

    using NativeCode.Clients.Radarr.Responses;

    public class MoviesSearchOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.MoviesSearch;

        public IEnumerable<int> MovieIds { get; set; }
    }
}
