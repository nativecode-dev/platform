namespace NativeCode.Node.Media.Extensions
{
    using System.IO;
    using System.Security.Cryptography;
    using Data.Storage;

    public static class MediaFileExtensions
    {
        public static void SetFileInfo(this MountPathFile source, FileInfo file)
        {
            using (var stream = file.OpenRead())
            using (var hash = SHA1.Create())
            {
                source.FileName = file.Name;
                source.FilePath = file.DirectoryName;
                source.Hash = hash.ComputeHash(stream);
                source.Size = file.Length;
            }
        }
    }
}
