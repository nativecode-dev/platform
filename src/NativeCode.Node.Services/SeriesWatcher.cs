namespace NativeCode.Node.Services
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Clients;
    using Clients.Sonarr;
    using Clients.Sonarr.Requests;
    using Core.Messaging;
    using Messages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class SeriesWatcher : ReleaseWatcher<SeriesRelease>
    {
        public SeriesWatcher(IOptions<SeriesWatcherOptions> options, IQueueManager queue, ILogger<SeriesRelease> logger, IMapper mapper,
            IClientFactory<SonarrClient> factory)
            : base(options, queue, logger, mapper)
        {
            this.Client = factory.CreateClient(new Uri(this.Options.Endpoint), this.Options.ApiKey);
        }

        protected SonarrClient Client { get; }

        protected override async Task Process(SeriesRelease message)
        {
            try
            {
                this.Logger.LogInformation($"Pushing release: {message.Name} {message.Link}");
                var success = await this.Client.Series.PushRelease(this.Mapper.Map<SeriesReleaseInfo>(message));

                if (success)
                {
                    this.Queue.Acknowledge(message.DeliveryTag);
                }
                else
                {
                    this.Queue.Requeue(message.DeliveryTag);
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
