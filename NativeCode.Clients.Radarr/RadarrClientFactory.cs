namespace NativeCode.Clients.Radarr
{
    using System;

    using NativeCode.Core;

    public class RadarrClientFactory : IClientFactory<RadarrClient>
    {
        public RadarrClientFactory(IObjectSerializer serializer)
        {
            this.Serializer = serializer;
        }

        protected IObjectSerializer Serializer { get; }

        public RadarrClient CreateClient(Uri baseUrl)
        {
            return new RadarrClient(this.Serializer, baseUrl);
        }
    }
}