namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using NativeCode.Clients.Radarr.Responses;

    public class RssSyncOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.RssSync;
    }
}
