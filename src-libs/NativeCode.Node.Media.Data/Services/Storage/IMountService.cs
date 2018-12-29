namespace NativeCode.Node.Media.Data.Services.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Enums;
    using Data.Storage;
    using JetBrains.Annotations;

    public interface IMountService
    {
        [NotNull]
        Task<Mount> CreateMount([NotNull] string name, MountType type, CancellationToken cancellationToken = default);

        [NotNull]
        Task<MountPath> CreateMountPath(Guid mountId, CancellationToken cancellationToken = default);

        [NotNull]
        Task<MountPath> CreateMountPath(Guid mountId, [NotNull] string filepath, CancellationToken cancellationToken = default);

        Task DeleteMount(Guid mountId, CancellationToken cancellationToken = default);

        [NotNull]
        Task<IEnumerable<Mount>> GetLocalMounts(CancellationToken cancellationToken = default);

        [NotNull]
        Task<Mount> GetMount(Guid mountId, CancellationToken cancellationToken = default);

        [NotNull]
        Task<MountPath> GetMountPath(Guid mountId, CancellationToken cancellationToken = default);

        [NotNull]
        Task<MountPath> GetMountPath(Guid mountId, [NotNull] string filepath, CancellationToken cancellationToken = default);

        [NotNull]
        Task<MountPath> GetMountPathById(Guid mountPathId, CancellationToken cancellationToken);

        [NotNull]
        Task<IEnumerable<Mount>> GetMounts(CancellationToken cancellationToken = default);

        [NotNull]
        Task<IEnumerable<Mount>> GetNfsMounts(CancellationToken cancellationToken = default);

        [NotNull]
        Task<IEnumerable<Mount>> GetSmbMounts(CancellationToken cancellationToken = default);
    }
}
