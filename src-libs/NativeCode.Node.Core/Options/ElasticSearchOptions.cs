namespace NativeCode.Node.Core.Options
{
    using System.Diagnostics.CodeAnalysis;

    public class ElasticSearchOptions
    {
        [SuppressMessage("Microsoft.Design", "CA1056")]
        public string Url { get; set; } = "http://elasticsearch:9200";
    }
}
