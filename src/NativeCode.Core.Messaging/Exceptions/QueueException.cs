namespace NativeCode.Core.Messaging.Exceptions
{
    using System;

    public abstract class QueueException : Exception
    {
        protected QueueException(string message)
            : base(message)
        {
        }

        protected QueueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}