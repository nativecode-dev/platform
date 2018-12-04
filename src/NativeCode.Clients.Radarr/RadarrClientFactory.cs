namespace NativeCode.Clients.Radarr
{
    using System;
    using Core.Serialization;

    public class RadarrClientFactory : IClientFactory<RadarrClient>
    {
        public RadarrClientFactory(IObjectSerializer serializer)
        {
            this.Serializer = serializer;
        }

        protected IObjectSerializer Serializer { get; }

        public RadarrClient CreateClient(Uri address)
        {
            return new RadarrClient(this.Serializer, address);
        }

        public RadarrClient CreateClient(Uri address, string apikey)
        {
            var client = this.CreateClient(address);
            client.SetApiKey(apikey);

            return client;
        }
    }
}
