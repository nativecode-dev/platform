namespace NativeCode.Node.Media.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Storage;

    public interface IMountService
    {
        Task<Mount> CreateMount(MountType type, string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<MountPath> CreateMountPath(Guid mountId, CancellationToken cancellationToken = default(CancellationToken));

        Task<MountPath> CreateMountPath(Guid mountId, string filepath, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Mount>> GetLocalMounts(CancellationToken cancellationToken = default(CancellationToken));

        Task<MountPath> GetMountPath(Guid mountId, CancellationToken cancellationToken = default(CancellationToken));

        Task<MountPath> GetMountPath(Guid mountId, string filepath, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Mount>> GetMounts(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Mount>> GetNfsMounts(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Mount>> GetSmbMounts(CancellationToken cancellationToken = default(CancellationToken));
    }
}
