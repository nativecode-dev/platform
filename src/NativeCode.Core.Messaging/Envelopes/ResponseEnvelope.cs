namespace NativeCode.Core.Messaging.Envelopes
{
    public class ResponseEnvelope<T> : QueueMessage
        where T : IQueueMessage
    {
        public ResponseEnvelope(T response)
        {
            this.Response = response;
        }

        public T Response { get; }
    }
}