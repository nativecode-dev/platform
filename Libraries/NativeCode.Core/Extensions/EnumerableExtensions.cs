namespace NativeCode.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> callback)
        {
            foreach (var item in enumerable)
            {
                callback(item);
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Func<T, T> callback)
        {
            return enumerable.Select(callback);
        }
    }
}
