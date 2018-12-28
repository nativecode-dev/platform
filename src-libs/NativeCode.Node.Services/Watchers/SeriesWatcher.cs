namespace NativeCode.Node.Services.Watchers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NativeCode.Clients;
    using NativeCode.Clients.Sonarr;
    using NativeCode.Clients.Sonarr.Requests;
    using NativeCode.Core.Messaging;
    using NativeCode.Node.Messages;

    public class SeriesWatcher : ReleaseWatcher<SeriesRelease>
    {
        public SeriesWatcher(
            IOptions<SeriesWatcherOptions> options,
            IQueueManager queue,
            ILogger<SeriesRelease> logger,
            IMapper mapper,
            IClientFactory<SonarrClient> factory)
            : base(options, queue, logger, mapper)
        {
            this.Client = factory.CreateClient(new Uri(this.Options.Endpoint), this.Options.ApiKey);
        }

        protected SonarrClient Client { get; }

        protected override Task<bool> PushRelease(SeriesRelease message)
        {
            return this.Client.Movie.PushRelease(this.Mapper.Map<SeriesReleaseInfo>(message));
        }
    }
}
