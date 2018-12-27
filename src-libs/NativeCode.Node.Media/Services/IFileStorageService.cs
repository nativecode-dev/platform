namespace NativeCode.Node.Media.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Storage;

    public interface IFileStorageService
    {
        Task CreateFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<MountPathFile>> GetFiles(Guid mountPathId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task RenameFile(Guid mountPathId, string filepath, string filename, FileInfo fileinfo,
            CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken = default(CancellationToken));
    }
}
