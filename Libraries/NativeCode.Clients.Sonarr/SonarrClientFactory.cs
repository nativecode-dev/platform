namespace NativeCode.Clients.Sonarr
{
    using System;

    using NativeCode.Core;

    public class SonarrClientFactory : IClientFactory<SonarrClient>
    {
        public SonarrClientFactory(IObjectSerializer serializer)
        {
            this.Serializer = serializer;
        }

        protected IObjectSerializer Serializer { get; }

        public SonarrClient CreateClient(Uri baseUrl)
        {
            return new SonarrClient(this.Serializer, baseUrl);
        }
    }
}
