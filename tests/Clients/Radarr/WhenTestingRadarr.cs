namespace NativeCode.Tests.Clients.Radarr
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Radarr.Extensions;
    using NativeCode.Core;

    public abstract class WhenTestingRadarr : WhenTestingDependencies
    {
        protected const string ServerAddress = "http://radarr.in.nativecode.com";

        protected static readonly string ApiKey = Environment.GetEnvironmentVariable("APIKEY_RADARR");

        protected WhenTestingRadarr()
        {
            var serializer = new JsonObjectSerializer();
            this.Client = new RadarrClient(serializer, new Uri(ServerAddress)).SetApiKey(ApiKey);
        }

        protected RadarrClient Client { get; }

        protected override IServiceCollection RegisterServices(IServiceCollection services)
        {
            return services.AddRadarrClient();
        }

        protected override void ReleaseManaged()
        {
        }
    }
}
