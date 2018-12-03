namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using Responses;

    public class NetImportSyncOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.NetImportSync;
    }
}