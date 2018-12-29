namespace NativeCode.Core.Messaging
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class QueueConsumer<T>
        where T : IQueueMessage
    {
        protected QueueConsumer(IQueueTopic<T> queue)
        {
            this.Queue = queue;
        }

        protected IQueueTopic<T> Queue { get; }

        protected TaskCompletionSource<bool> Source { get; private set; }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            if (this.Source != null)
            {
                throw new InvalidOperationException("Cannot start another task when one is running.");
            }

            this.Source = new TaskCompletionSource<bool>();

            this.Queue.AsObservable()
                .Subscribe(
                    async value => await this.ProcessMessage(value)
                        .ConfigureAwait(false),
                    this.ProcessCompleted,
                    cancellationToken);

            return this.Source.Task;
        }

        public virtual Task StopAsync()
        {
            this.Source?.SetResult(true);
            this.Source = null;

            return this.Source?.Task;
        }

        protected virtual void ProcessCompleted()
        {
            this.Source.SetResult(true);
        }

        protected abstract Task ProcessMessage(T message);
    }
}
