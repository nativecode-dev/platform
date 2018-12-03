namespace NativeCode.Clients.Sonarr
{
    using System;
    using Core.Serialization;
    using Resources;
    using RestSharp;

    public class SonarrClient : CommonClient
    {
        public SonarrClient(IObjectSerializer serializer, Uri baseAddress)
            : base(serializer, ValidateUri(baseAddress))
        {
            this.Series = new SeriesResource(this.Client, this.Serializer);
        }

        public SeriesResource Series { get; }

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

            if (uri.PathAndQuery.StartsWith("/api") == false)
            {
                return new Uri(uri, "api/");
            }

            return uri;
        }
    }
}