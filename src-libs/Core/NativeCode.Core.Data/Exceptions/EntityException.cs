namespace NativeCode.Core.Data.Exceptions
{
    using System;

    public abstract class EntityException : Exception
    {
        protected EntityException()
        {
        }

        protected EntityException(string message)
            : base(message)
        {
        }

        protected EntityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class EntityException<TEntity> : EntityException
        where TEntity : IEntity
    {
        public EntityException()
        {
        }

        public EntityException(string message)
            : base(message)
        {
        }

        public EntityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
