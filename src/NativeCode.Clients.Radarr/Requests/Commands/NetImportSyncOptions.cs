namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using NativeCode.Clients.Radarr.Responses;

    public class NetImportSyncOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.NetImportSync;
    }
}
