namespace NativeCode.Clients.Radarr
{
    using System;

    using NativeCode.Core;

    using RestSharp;

    public class RadarrClient : ClientBase
    {
        public RadarrClient(IObjectSerializer serializer, Uri baseUrl)
            : base(serializer, ValidateUri(baseUrl))
        {
        }

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
