namespace NativeCode.Clients.Radarr
{
    using System;

    using NativeCode.Clients.Radarr.Resources;
    using NativeCode.Core;

    using RestSharp;

    public class RadarrClient : CommonRestClient
    {
        public RadarrClient(IObjectSerializer serializer, Uri baseUrl)
            : base(serializer, ValidateUri(baseUrl))
        {
            this.Calendar = new CalendarResource(this.Client, this.Serializer);
            this.Commands = new CommandResource(this.Client, this.Serializer);
            this.DiskSpace = new DiskSpaceResource(this.Client, this.Serializer);
            this.Movies = new MovieResource(this.Client, this.Serializer);
            this.System = new SystemResource(this.Client, this.Serializer);
        }

        public CalendarResource Calendar { get; }

        public CommandResource Commands { get; }

        public DiskSpaceResource DiskSpace { get; }

        public MovieResource Movies { get; }

        public SystemResource System { get; }

        public RadarrClient SetApiKey(string apikey)
        {
            this.Client.AddDefaultHeader("X-Api-Key", apikey);
            return this;
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
