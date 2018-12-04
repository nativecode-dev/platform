namespace NativeCode.Core.Messaging.Exceptions
{
    using System;

    public class QueueSerializationException : QueueException
    {
        public QueueSerializationException(Type type)
            : base(CreateExceptionMessage(type))
        {
        }

        public QueueSerializationException(Type type, Exception innerException)
            : base(CreateExceptionMessage(type), innerException)
        {
        }

        private static string CreateExceptionMessage(Type type)
        {
            return $"Unable to serialize {type.Name}";
        }
    }

    public class QueueSerializationException<T> : QueueSerializationException
    {
        public QueueSerializationException() : base(typeof(T))
        {
        }

        public QueueSerializationException(Exception innerException) : base(typeof(T), innerException)
        {
        }
    }
}
