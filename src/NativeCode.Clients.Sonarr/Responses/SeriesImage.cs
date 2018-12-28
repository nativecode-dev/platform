namespace NativeCode.Clients.Sonarr.Responses
{
    using System.Diagnostics.CodeAnalysis;

    public class SeriesImage
    {
        public string CoverType { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1056")]
        public string Url { get; set; }
    }
}
