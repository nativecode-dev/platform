namespace NativeCode.Core.Extensions
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class TaskExceptions
    {
        public static ConfiguredTaskAwaitable Capture(this Task task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable<T> Capture<T>(this Task<T> task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable NoCapture(this Task task)
        {
            return task.ConfigureAwait(false);
        }

        public static ConfiguredTaskAwaitable<T> NoCapture<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}
