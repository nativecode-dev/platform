namespace NativeCode.Core.Messaging
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Reliability;

    public abstract class QueuePattern<T> : Disposable, IQueueTopic<T>
        where T : IQueueMessage
    {
        protected QueuePattern(IQueueSerializer serializer, ILogger<T> logger)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        protected ILogger<T> Logger { get; }

        protected IQueueSerializer Serializer { get; }

        public string QueueName { get; protected set; }

        public string Route { get; protected set; }

        public abstract IObservable<T> AsObservable();

        public abstract Task Publish(T message);
    }
}