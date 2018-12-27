namespace NativeCode.Node.Media.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Storage;
    using Microsoft.EntityFrameworkCore;
    using NativeCode.Core.Data.Exceptions;

    public class MountService : IMountService
    {
        public const string RootPath = "/";

        public MountService(MediaDataContext context)
        {
            this.Context = context;
        }

        public MediaDataContext Context { get; }

        /// <inheritdoc />
        public async Task<Mount> CreateMount(MountType type, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mount = await this.Context.Mounts.SingleOrDefaultAsync(m => m.Name == name, cancellationToken);

            if (mount != null)
            {
                throw new EntityExistsException<Mount>(name);
            }

            var entity = new Mount
            {
                Name = name,
                MountPaths = new List<MountPath>
                {
                    new MountPath
                    {
                        Path = RootPath,
                    },
                },
                MountType = type,
            };

            this.Context.Mounts.Add(entity);

            return entity;
        }

        /// <inheritdoc />
        public Task<MountPath> CreateMountPath(Guid mountId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.CreateMountPath(mountId, RootPath, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<MountPath> CreateMountPath(Guid mountId, string filepath,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var mount = await this.Context.Mounts.Include(m => m.MountPaths)
                .SingleAsync(m => m.Id == mountId, cancellationToken);

            if (mount.MountPaths.Any(mp => mp.Path == filepath))
            {
                throw new EntityExistsException<MountPath>(filepath);
            }

            var path = new MountPath
            {
                Path = filepath,
                Mount = mount,
            };

            mount.MountPaths.Add(path);

            await this.Context.SaveChangesAsync(cancellationToken);

            return path;
        }

        /// <inheritdoc />
        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetLocalMounts(CancellationToken cancellationToken)
        {
            return await this.Context.Mounts
                .Include(m => m.Credential)
                .Include(m => m.MountPaths)
                .Where(m => m.MountType == MountType.Local)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<MountPath> GetMountPath(Guid mountId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetMountPath(mountId, RootPath, cancellationToken);
        }

        /// <inheritdoc />
        public Task<MountPath> GetMountPath(Guid mountId, string filepath, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Context.Mounts
                .Where(m => m.Id == mountId)
                .SelectMany(m => m.MountPaths)
                .SingleAsync(mp => mp.Path == filepath, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetMounts(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.Context.Mounts
                .Include(m => m.Credential)
                .Include(m => m.MountPaths)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetNfsMounts(CancellationToken cancellationToken)
        {
            return await this.Context.Mounts
                .Include(m => m.Credential)
                .Include(m => m.MountPaths)
                .Where(m => m.MountType == MountType.NetworkFileShare)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetSmbMounts(CancellationToken cancellationToken)
        {
            return await this.Context.Mounts
                .Include(m => m.Credential)
                .Include(m => m.MountPaths)
                .Where(m => m.MountType == MountType.ServerMessageBlock)
                .ToListAsync(cancellationToken);
        }
    }
}
