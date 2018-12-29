namespace NativeCode.Node.Media.Services.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Enums;
    using Data;
    using Data.Data.Storage;
    using Data.Services.Storage;
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
        public async Task<Mount> CreateMount(string name, MountType type, CancellationToken cancellationToken = default)
        {
            var mount = await this.Context.Mounts.SingleOrDefaultAsync(m => m.Name == name, cancellationToken)
                .ConfigureAwait(false);

            if (mount != null)
            {
                throw new EntityExistsException<Mount>(name);
            }

            var entity = new Mount {Name = name, Type = type, };

            entity.Paths.Add(new MountPath {Path = RootPath, });

            this.Context.Mounts.Add(entity);

            return entity;
        }

        /// <inheritdoc />
        public Task<MountPath> CreateMountPath(Guid mountId, CancellationToken cancellationToken = default)
        {
            return this.CreateMountPath(mountId, RootPath, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<MountPath> CreateMountPath(Guid mountId, string filepath, CancellationToken cancellationToken = default)
        {
            var mount = await this.Context.Mounts.Include(m => m.Paths)
                .SingleAsync(m => m.Id == mountId, cancellationToken)
                .ConfigureAwait(false);

            if (mount.Paths.Any(mp => mp.Path == filepath))
            {
                throw new EntityExistsException<MountPath>(filepath);
            }

            var path = new MountPath {Path = filepath, Mount = mount, };

            mount.Paths.Add(path);

            await this.Context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return path;
        }

        /// <inheritdoc />
        public async Task DeleteMount(Guid mountId, CancellationToken cancellationToken = default)
        {
            var mount = await this.GetMount(mountId, cancellationToken)
                .ConfigureAwait(false);

            this.Context.Mounts.Remove(mount);
        }

        /// <inheritdoc />
        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetLocalMounts(CancellationToken cancellationToken)
        {
            return await this.Context.Mounts.Include(m => m.Credentials)
                .Include(m => m.Paths)
                .Where(m => m.Type == MountType.Local)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<Mount> GetMount(Guid mountId, CancellationToken cancellationToken = default)
        {
            return this.Context.Mounts.SingleAsync(m => m.Id == mountId, cancellationToken);
        }

        /// <inheritdoc />
        public Task<MountPath> GetMountPath(Guid mountId, CancellationToken cancellationToken = default)
        {
            return this.GetMountPath(mountId, RootPath, cancellationToken);
        }

        /// <inheritdoc />
        public Task<MountPath> GetMountPath(Guid mountId, string filepath, CancellationToken cancellationToken = default)
        {
            return this.Context.Mounts.SelectMany(m => m.Paths)
                .Include(mp => mp.Mount)
                .SingleAsync(mp => mp.MountId == mountId && mp.Path == filepath, cancellationToken);
        }

        public Task<MountPath> GetMountPathById(Guid mountPathId, CancellationToken cancellationToken)
        {
            return this.Context.Mounts.SelectMany(m => m.Paths)
                .SingleAsync(mp => mp.Id == mountPathId, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetMounts(CancellationToken cancellationToken = default)
        {
            return await this.Context.Mounts.Include(m => m.Credentials)
                .Include(m => m.Paths)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetNfsMounts(CancellationToken cancellationToken)
        {
            return await this.Context.Mounts.Include(m => m.Credentials)
                .Include(m => m.Paths)
                .Where(m => m.Type == MountType.Nfs)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Mount>> GetSmbMounts(CancellationToken cancellationToken)
        {
            return await this.Context.Mounts.Include(m => m.Credentials)
                .Include(m => m.Paths)
                .Where(m => m.Type == MountType.Smb)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
