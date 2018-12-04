namespace NativeCode.Clients.Posteio.Responses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class Domain
    {
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [JsonProperty("domain_bin")]
        public bool DomainBin { get; set; }

        [JsonProperty("domain_bin_address")]
        public string DomainBinAddress { get; set; }

        public bool Forward { get; set; }

        [JsonProperty("forward_domain")]
        public string ForwardDomain { get; set; }

        public string Home { get; set; }

        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; }
    }
}
