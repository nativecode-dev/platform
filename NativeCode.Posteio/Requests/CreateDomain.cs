namespace NativeCode.Posteio.Requests
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class CreateDomain
    {
        public bool DomainBin { get; set; }

        public string DomainBinAddress { get; set; }

        [Required]
        [JsonProperty("name")]
        public string DomainName { get; set; }

        public bool Forward { get; set; }

        public string ForwardDomain { get; set; }
    }
}
