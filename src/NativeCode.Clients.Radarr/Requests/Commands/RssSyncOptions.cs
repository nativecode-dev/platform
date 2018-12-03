namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using Responses;

    public class RssSyncOptions : CommandOptions
    {
        public override CommandKind Name => CommandKind.RssSync;
    }
}