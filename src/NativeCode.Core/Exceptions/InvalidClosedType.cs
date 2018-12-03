namespace NativeCode.Core.Exceptions
{
    using System;

    public class InvalidClosedType : Exception
    {
        public InvalidClosedType(Type type)
            : base(CreateDefaultMessage(type))
        {
        }

        public static string CreateDefaultMessage(Type type)
        {
            return $"{type.FullName} is not a closed type";
        }
    }
}