namespace NativeCode.Core.Messaging.Envelopes
{
    public class RequestEnvelope<T> : QueueMessage
        where T : IQueueMessage
    {
        public RequestEnvelope(T message)
        {
            this.Message = message;
        }

        public T Message { get; }
    }
}
