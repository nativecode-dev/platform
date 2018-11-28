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

        public virtual Task Start(CancellationToken cancellationToken)
        {
            if (this.Source != null)
            {
                throw new InvalidOperationException("Cannot start another task when one is running.");
            }

            this.Source = new TaskCompletionSource<bool>();

            this.Queue.AsObservable()
                .Subscribe(async value => await this.ProcessMessage(value), this.ProcessCompleted, cancellationToken);

            return this.Source.Task;
        }

        public virtual void Stop()
        {
            this.Source?.SetResult(true);
            this.Source = null;
        }

        protected virtual void ProcessCompleted()
        {
            this.Source.SetResult(true);
        }

        protected abstract Task ProcessMessage(T message);
    }
}