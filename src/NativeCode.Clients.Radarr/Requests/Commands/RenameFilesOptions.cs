namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using System.Collections.Generic;
    using Responses;

    public class RenameFilesOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.RenameFiles;

        public IEnumerable<int> MovieIds { get; set; }
    }
}
