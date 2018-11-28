namespace NativeCode.Core.Exceptions
{
    using System;

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EntityNotFoundException(int id)
            : base(CreateExceptionMessageFromId(id))
        {
        }

        public EntityNotFoundException(int id, Exception innerException)
            : base(CreateExceptionMessageFromId(id), innerException)
        {
        }

        public EntityNotFoundException(Type entityType, int id)
            : base(CreateExceptionMessageFromTypeAndId(entityType, id))
        {
        }

        private static string CreateExceptionMessageFromId(int id)
        {
            return $"Could not find entity with id {id}.";
        }

        private static string CreateExceptionMessageFromTypeAndId(Type type, int id)
        {
            return $"Could not find {type.Name} with id {id}.";
        }
    }
}