namespace NativeCode.Clients.Radarr.Requests.Commands
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Responses;

    public abstract class CommandOptions
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public abstract CommandKind Name { get; }
    }
}