namespace NativeCode.Clients.Radarr.Responses.Commands
{
    using System.Collections.Generic;

    public class RenameFilesOptions : CommandOptions
    {
        public override CommandKind Command => CommandKind.RenameFiles;

        public IEnumerable<int> MovieIds { get; set; }
    }
}
