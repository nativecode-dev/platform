namespace NativeCode.Core.Messaging
{
    using System;

    /// <summary>
    /// Provides an entry-point for creating queue topics.
    /// </summary>
    public interface IQueueManager : IDisposable
    {
        IQueueTopic<T> GetIncomingQueue<T>()
            where T : IQueueMessage;

        IQueueTopic<T> GetIncomingQueue<T>(string queue, string route = default(string))
            where T : IQueueMessage;

        IQueueTopic<T> GetOutgoingQueue<T>()
            where T : IQueueMessage;

        IQueueTopic<T> GetOutgoingQueue<T>(string queue, string route = default(string))
            where T : IQueueMessage;
    }
}