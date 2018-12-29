namespace NativeCode.Core.Exceptions
{
    using System;

    public class InvalidClosedTypeException : Exception
    {
        public InvalidClosedTypeException()
        {
        }

        public InvalidClosedTypeException(Type type)
            : base(CreateDefaultMessage(type))
        {
        }

        public InvalidClosedTypeException(string message)
            : base(message)
        {
        }

        public InvalidClosedTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static string CreateDefaultMessage(Type type)
        {
            return $"{type.FullName} is not a closed type";
        }
    }
}
