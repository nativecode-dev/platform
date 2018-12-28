namespace NativeCode.Node.Media.Data.Services.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using NativeCode.Node.Media.Data.Data.Storage;

    public interface IFileService
    {
        Task CreateFile(Guid mountPathId, [NotNull] FileInfo fileinfo, CancellationToken cancellationToken = default);

        Task DeleteFile(Guid mountPathId, [NotNull] FileInfo fileinfo, CancellationToken cancellationToken = default);

        [NotNull]
        Task<IEnumerable<MountPathFile>> GetFiles(Guid mountPathId, CancellationToken cancellationToken = default);

        Task<IEnumerable<MountPathFile>> ImportFiles(MountPath mount, CancellationToken cancellationToken = default);

        Task RenameFile(
            Guid mountPathId,
            [NotNull] string filepath,
            [NotNull] string filename,
            [NotNull] FileInfo fileinfo,
            CancellationToken cancellationToken = default);

        Task UpdateFile(Guid mountPathId, [NotNull] FileInfo fileinfo, CancellationToken cancellationToken = default);
    }
}
