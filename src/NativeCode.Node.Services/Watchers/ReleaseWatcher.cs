namespace NativeCode.Node.Services.Watchers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Messaging;
    using Core.Services;
    using Messages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Nito.AsyncEx;

    public abstract class ReleaseWatcher<T> : HostedService<ReleaseWatcherOptions>, IObserver<T> where T : IrcRelease
    {
        protected ReleaseWatcher(IOptions<ReleaseWatcherOptions> options, IQueueManager queue, ILogger<T> logger, IMapper mapper) : base(options)
        {
            this.Logger = logger;
            this.Mapper = mapper;
            this.Queue = queue.GetIncomingQueue<T>();
        }

        protected ILogger<T> Logger { get; }

        protected IMapper Mapper { get; }

        protected IQueueTopic<T> Queue { get; }

        protected IDisposable Subscription { get; private set; }

        public virtual void OnCompleted()
        {
            this.Subscription?.Dispose();
        }

        public virtual void OnError(Exception error)
        {
        }

        public virtual void OnNext(T message)
        {
            AsyncContext.Run(async () => await this.Process(message));
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.Subscription?.Dispose();
            this.Subscription = this.Queue.AsObservable()
                .Subscribe(this);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.Subscription?.Dispose();
            return Task.CompletedTask;
        }

        protected override void ReleaseManaged()
        {
            this.Subscription?.Dispose();
        }

        protected async Task Process(T message)
        {
            try
            {
                this.Logger.LogInformation($"Pushing: [{message.GetType().Name}] {{@message}}", message);

                var success = await this.PushRelease(message);

                if (success || string.IsNullOrWhiteSpace(message.Name) == false)
                {
                    this.Queue.Acknowledge(message.DeliveryTag);
                    message.TargetMachine = Environment.MachineName;
                    this.Logger.LogInformation($"Pushed: [{message.GetType().Name}] {{@message}}", message);
                }
                else
                {
                    this.Queue.Requeue(message.DeliveryTag);
                    this.Logger.LogInformation($"Requeued: [{message.GetType().Name}] {{@message}}", message);
                }
            }
            catch (Exception ex)
            {
                this.Queue.Requeue(message.DeliveryTag);
                this.Logger.LogError(ex, ex.Message);
            }
        }

        protected abstract Task<bool> PushRelease(T message);
    }
}