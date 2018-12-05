namespace NativeCode.Node.Services
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

    public abstract class ReleaseWatcher<T> : HostedService<ReleaseOptions>, IObserver<T> where T : IrcRelease
    {
        protected ReleaseWatcher(IOptions<ReleaseOptions> options, IQueueManager queue, ILogger<T> logger, IMapper mapper) : base(options)
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

        public virtual void OnNext(T value)
        {
            AsyncContext.Run(async () =>
            {
                this.Logger.LogInformation("Pushing: {@value}", value);
                await this.Process(value);
            });
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

        protected abstract Task Process(T message);
    }
}
