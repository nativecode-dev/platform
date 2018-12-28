namespace NativeCode.Core.Data.Exceptions
{
    using System;

    public class EntityException<TEntity> : Exception where TEntity : IEntity
    {
        protected EntityException()
        {
        }

        protected EntityException(string message) : base(message)
        {
        }

        protected EntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
