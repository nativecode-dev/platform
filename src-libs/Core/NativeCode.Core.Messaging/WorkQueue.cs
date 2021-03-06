namespace NativeCode.Core.Messaging
{
    using System.Threading;
    using System.Threading.Tasks;
    using Reliability;

    public abstract class WorkQueue<TRequest, TResult> : Disposable, IWorkQueue<TRequest, TResult>
        where TRequest : IQueueMessage
        where TResult : IQueueMessage
    {
        private CancellationTokenSource cancellationTokenSource;

        protected WorkQueue(IQueueManager manager)
        {
            this.Inbox = manager.GetIncomingQueue<TRequest>();
            this.Outbox = manager.GetOutgoingQueue<TResult>();
        }

        public abstract IWorkConsumer<TRequest> Consumer { get; }

        public abstract IWorkDispatcher<TResult> Dispatcher { get; }

        protected IQueueTopic<TRequest> Inbox { get; }

        protected IQueueTopic<TResult> Outbox { get; }

        public Task Publish(TRequest request)
        {
            return this.Inbox.Publish(request);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            cancellationToken.Register(() => this.cancellationTokenSource.Cancel());
            return Task.WhenAll(this.Consumer.StartAsync(cancellationToken), this.Dispatcher.StartAsync(cancellationToken));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.cancellationTokenSource?.Cancel();

            return Task.CompletedTask;
        }

        protected override void ReleaseManaged()
        {
            this.Outbox.Dispose();
            this.Inbox.Dispose();
        }
    }
}
