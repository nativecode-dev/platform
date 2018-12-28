namespace NativeCode.Core.Extensions
{
    using System.IO;

    public static class StreamExtensions
    {
        public static TStream Rewind<TStream>(this TStream stream)
            where TStream : Stream
        {
            stream.Position = 0;

            return stream;
        }
    }
}
