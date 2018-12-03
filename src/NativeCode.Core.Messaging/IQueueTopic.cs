namespace NativeCode.Core.Messaging
{
    using System;
    using System.Threading.Tasks;

    public interface IQueueTopic<T> : IDisposable
        where T : IQueueMessage
    {
        string QueueName { get; }

        string Route { get; }

        void Acknowledge(ulong deliveryTag);

        void Requeue(ulong deliveryTag);

        IObservable<T> AsObservable();

        Task Publish(T message);
    }
}
