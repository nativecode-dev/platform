namespace NativeCode.Clients.Sonarr
{
    using System;
    using Core.Serialization;

    public class SonarrClientFactory : IClientFactory<SonarrClient>
    {
        public SonarrClientFactory(IObjectSerializer serializer)
        {
            this.Serializer = serializer;
        }

        protected IObjectSerializer Serializer { get; }

        public SonarrClient CreateClient(Uri address)
        {
            return new SonarrClient(this.Serializer, address);
        }

        public SonarrClient CreateClient(Uri address, string apikey)
        {
            var client = this.CreateClient(address);
            client.SetApiKey(apikey);

            return client;
        }
    }
}
