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

        protected IQueueTopic<TRequest> Inbox { get; }

        protected IQueueTopic<TResult> Outbox { get; }

        public abstract IWorkConsumer<TRequest> Consumer { get; }

        public abstract IWorkDispatcher<TResult> Dispatcher { get; }

        public Task Publish(TRequest request)
        {
            return this.Inbox.Publish(request);
        }

        public Task Start(CancellationToken cancellationToken)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            cancellationToken.Register(() => this.cancellationTokenSource.Cancel());
            return Task.WhenAll(this.Consumer.Start(cancellationToken), this.Dispatcher.Start(cancellationToken));
        }

        public void Stop()
        {
            this.cancellationTokenSource?.Cancel();
        }

        protected override void ReleaseManaged()
        {
            this.Outbox.Dispose();
            this.Inbox.Dispose();
        }
    }
}