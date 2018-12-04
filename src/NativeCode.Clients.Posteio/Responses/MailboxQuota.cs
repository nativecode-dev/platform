namespace NativeCode.Clients.Posteio.Responses
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class MailboxQuota
    {
        [JsonProperty("count_limit")]
        public int CountLimit { get; set; }

        [JsonProperty("count_usage")]
        public int CountUsage { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [JsonProperty("storage_limit")]
        public int StorageLimit { get; set; }

        [JsonProperty("storage_usage")]
        public int StorageUsage { get; set; }
    }
}
