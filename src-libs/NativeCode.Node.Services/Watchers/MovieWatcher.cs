namespace NativeCode.Node.Services.Watchers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NativeCode.Clients;
    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Radarr.Requests;
    using NativeCode.Core.Messaging;
    using NativeCode.Node.Messages;

    public class MovieWatcher : ReleaseWatcher<MovieRelease>
    {
        public MovieWatcher(
            IOptions<MovieWatcherOptions> options,
            IQueueManager queue,
            ILogger<MovieRelease> logger,
            IMapper mapper,
            IClientFactory<RadarrClient> factory)
            : base(options, queue, logger, mapper)
        {
            this.Client = factory.CreateClient(new Uri(this.Options.Endpoint), this.Options.ApiKey);
        }

        protected RadarrClient Client { get; }

        protected override Task<bool> PushRelease(MovieRelease message)
        {
            return this.Client.Movies.PushRelease(this.Mapper.Map<MovieReleaseInfo>(message));
        }
    }
}
