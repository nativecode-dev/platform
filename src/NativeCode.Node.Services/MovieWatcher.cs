namespace NativeCode.Node.Services
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Clients;
    using Clients.Radarr;
    using Clients.Radarr.Requests;
    using Core.Messaging;
    using Messages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class MovieWatcher : ReleaseWatcher<MovieRelease>
    {
        public MovieWatcher(IOptions<MovieWatcherOptions> options, IQueueManager queue, ILogger<MovieRelease> logger, IMapper mapper,
            IClientFactory<RadarrClient> factory)
            : base(options, queue, logger, mapper)
        {
            this.Client = factory.CreateClient(new Uri(this.Options.Endpoint), this.Options.ApiKey);
        }

        protected RadarrClient Client { get; }

        protected override async Task Process(MovieRelease message)
        {
            try
            {
                var success = await this.Client.Movies.PushRelease(this.Mapper.Map<MovieReleaseInfo>(message));

                if (success || string.IsNullOrWhiteSpace(message.Name) == false)
                {
                    this.Queue.Acknowledge(message.DeliveryTag);
                    this.Logger.LogInformation("Pushed: {@message}", message);
                }
                else
                {
                    this.Queue.Requeue(message.DeliveryTag);
                    this.Logger.LogInformation("Requeued: {@message}", message);
                }
            }
            catch (Exception ex)
            {
                this.Queue.Requeue(message.DeliveryTag);
                this.Logger.LogError(ex, ex.Message);
            }
        }
    }
}
