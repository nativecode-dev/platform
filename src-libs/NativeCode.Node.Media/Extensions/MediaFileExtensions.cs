namespace NativeCode.Node.Media.Extensions
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using Data.Storage;
    using NativeCode.Core.Extensions;

    public static class MediaFileExtensions
    {
        public const int MaxSegmentLength = 1024 * 1024 * 24;

        public const long MaxStreamLength = 1024 * 1024 * 100;

        public static void SetFileInfo(this MountPathFile source, FileInfo file, long maxStreamLength = MaxStreamLength,
            int maxSegmentLength = MaxSegmentLength)
        {
            using (var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var keystream = new MemoryStream())
            using (var hash = SHA1.Create())
            {
                source.FileName = file.Name;
                source.FilePath = file.DirectoryName;
                source.Size = file.Length;

                if (stream.Length <= maxStreamLength)
                {
                    source.Hash = hash.ComputeHash(stream);
                    return;
                }

                var buffer = new byte[maxSegmentLength];

                // Read the first segment of bytes and write to key stream.
                stream.Read(buffer, 0, maxSegmentLength);
                keystream.Write(buffer, 0, maxSegmentLength);

                // Read the last segment of bytes and write to key stream.
                var position = Math.Abs(stream.Length - maxSegmentLength);
                stream.Position = position;
                stream.Read(buffer, 0, maxSegmentLength);
                keystream.Write(buffer, 0, maxSegmentLength);

                source.Hash = hash.ComputeHash(keystream.Rewind());
            }
        }
    }
}
