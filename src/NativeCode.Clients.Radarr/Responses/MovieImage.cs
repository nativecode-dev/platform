namespace NativeCode.Clients.Radarr.Responses
{
    using System.Diagnostics.CodeAnalysis;

    public class MovieImage
    {
        public string CoverType { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1056")]
        public string Url { get; set; }
    }
}
