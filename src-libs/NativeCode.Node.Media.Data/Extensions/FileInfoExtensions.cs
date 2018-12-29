namespace NativeCode.Node.Media.Data.Extensions
{
    using System;
    using System.IO;
    using Data.Storage;

    public static class FileInfoExtensions
    {
        public static MountPathFile CreateMountPathFile(this FileInfo fileinfo, Guid mountPathId)
        {
            var mountfile = new MountPathFile {MountPathId = mountPathId, };

            mountfile.SetFileInfo(fileinfo);

            return mountfile;
        }
    }
}
