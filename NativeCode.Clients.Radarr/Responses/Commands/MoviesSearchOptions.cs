namespace NativeCode.Clients.Radarr.Responses.Commands
{
    using System.Collections.Generic;

    public class MoviesSearchOptions : CommandOptions
    {
        public override CommandKind Command => CommandKind.MoviesSearch;

        public IEnumerable<int> MovieIds { get; set; }
    }
}
