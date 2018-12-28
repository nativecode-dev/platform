namespace NativeCode.Clients.Posteio.Responses
{
    using Newtonsoft.Json;

    public class DomainKey
    {
        [JsonProperty("name")]
        public string DomainName { get; set; }

        public string Private { get; set; }

        public string Public { get; set; }

        public string Selector { get; set; }
    }
}
