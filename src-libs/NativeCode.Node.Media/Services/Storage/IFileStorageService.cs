namespace NativeCode.Node.Media.Services.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Storage;
    using JetBrains.Annotations;

    public interface IFileStorageService
    {
        Task CreateFile(Guid mountPathId, [NotNull] FileInfo fileinfo, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteFile(Guid mountPathId, [NotNull] FileInfo fileinfo, CancellationToken cancellationToken = default(CancellationToken));

        [NotNull]
        Task<IEnumerable<MountPathFile>> GetFiles(Guid mountPathId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task RenameFile(Guid mountPathId, [NotNull] string filepath, [NotNull] string filename, [NotNull] FileInfo fileinfo,
            CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateFile(Guid mountPathId, [NotNull] FileInfo fileinfo, CancellationToken cancellationToken = default(CancellationToken));
    }
}
