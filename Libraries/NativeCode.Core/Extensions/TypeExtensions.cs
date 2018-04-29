namespace NativeCode.Core.Extensions
{
    using System;
    using System.Linq;

    public static class TypeExtensions
    {
        public static string AsKey(this Type[] types, string separator = ":")
        {
            return string.Join(separator, types.Select(type => type.Name));
        }
    }
}
