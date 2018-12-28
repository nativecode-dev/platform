namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using NativeCode.Clients.Radarr.Responses;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public abstract class CommandOptions
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public abstract CommandKind Name { get; }
    }
}
