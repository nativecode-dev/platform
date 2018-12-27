namespace NativeCode.Core.Data.Exceptions
{
    using System;

    [Serializable]
    public class EntityExistsException<TEntity> : EntityException<TEntity> where TEntity : IEntity
    {
        public EntityExistsException(string message) : base(CreateExceptionMessage(message))
        {
        }

        public EntityExistsException(Exception innerException) : base(CreateExceptionMessage(innerException.Message), innerException)
        {
        }

        public EntityExistsException(string message, Exception innerException) : base(CreateExceptionMessage(message), innerException)
        {
        }

        private static string CreateExceptionMessage(string message)
        {
            return $"{typeof(TEntity).Name}: {message}";
        }
    }
}
