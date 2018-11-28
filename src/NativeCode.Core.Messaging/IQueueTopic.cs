namespace NativeCode.Core.Messaging
{
    using System;
    using System.Threading.Tasks;

    public interface IQueueTopic<T> : IDisposable
        where T : IQueueMessage
    {
        string QueueName { get; }

        string Route { get; }

        IObservable<T> AsObservable();

        Task Publish(T message);
    }
}