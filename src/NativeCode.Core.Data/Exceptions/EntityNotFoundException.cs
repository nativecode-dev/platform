namespace NativeCode.Core.Data.Exceptions
{
    using System;

    public class EntityNotFoundException<TEntity> : EntityException<TEntity> where TEntity : IEntity
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(CreateExceptionMessage(message))
        {
        }

        public EntityNotFoundException(Exception innerException) : base(CreateExceptionMessage(innerException.Message), innerException)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(CreateExceptionMessage(message), innerException)
        {
        }

        private static string CreateExceptionMessage(string message)
        {
            return $"{typeof(TEntity).Name}: {message}";
        }
    }
}
