namespace NativeCode.Clients.Sonarr
{
    using System;

    using NativeCode.Clients.Sonarr.Resources;
    using NativeCode.Core.Serialization;

    using RestSharp;

    public class SonarrClient : CommonClient
    {
        public SonarrClient(IObjectSerializer serializer, Uri baseAddress)
            : base(serializer, ValidateUri(baseAddress))
        {
            this.Movie = new MovieResource(this.Client, this.Serializer);
        }

        public MovieResource Movie { get; }

        public void SetApiKey(string apikey)
        {
            this.Client.AddDefaultHeader("X-Api-Key", apikey);
        }

        private static Uri ValidateUri(Uri uri)
        {
            if (string.IsNullOrWhiteSpace(uri.PathAndQuery))
            {
                return new Uri(uri, "api/");
            }

            if (uri.PathAndQuery.StartsWith("/api", StringComparison.InvariantCultureIgnoreCase) == false)
            {
                return new Uri(uri, "api/");
            }

            return uri;
        }
    }
}
