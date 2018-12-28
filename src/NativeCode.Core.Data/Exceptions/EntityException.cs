namespace NativeCode.Core.Data.Exceptions
{
    using System;

    [Serializable]
    public class EntityException<TEntity> : Exception where TEntity : IEntity
    {
        protected EntityException(string message) : base(message)
        {
        }

        protected EntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
