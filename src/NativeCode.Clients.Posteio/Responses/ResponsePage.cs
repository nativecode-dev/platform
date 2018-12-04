namespace NativeCode.Clients.Posteio.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class ResponsePage<T>
    {
        public int LastPage { get; set; }

        public int Page { get; set; }

        public int Paging { get; set; }

        public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();

        [JsonProperty("results_count")]
        public int ResultsCount { get; set; }
    }
}
