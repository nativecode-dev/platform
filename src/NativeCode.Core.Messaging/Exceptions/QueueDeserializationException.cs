namespace NativeCode.Core.Messaging.Exceptions
{
    using System;

    public class QueueDeserializationException : QueueException
    {
        public QueueDeserializationException()
        {
        }

        public QueueDeserializationException(string message) : base(message)
        {
        }

        public QueueDeserializationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public QueueDeserializationException(Type type)
            : base(CreateExceptionMessage(type))
        {
        }

        public QueueDeserializationException(Type type, Exception innerException)
            : base(CreateExceptionMessage(type), innerException)
        {
        }

        private static string CreateExceptionMessage(Type type)
        {
            return $"Unable to deserialize {type.Name}";
        }
    }

    public class QueueDeserializationException<T> : QueueDeserializationException
    {
        public QueueDeserializationException() : base(typeof(T), null)
        {
        }

        public QueueDeserializationException(string message) : base(message)
        {
        }

        public QueueDeserializationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public QueueDeserializationException(Exception innerException) : base(typeof(T), innerException)
        {
        }
    }
}
