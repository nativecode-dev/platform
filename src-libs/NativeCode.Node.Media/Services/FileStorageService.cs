namespace NativeCode.Node.Media.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Storage;
    using Extensions;
    using Microsoft.EntityFrameworkCore;

    public class FileStorageService : IFileStorageService
    {
        public FileStorageService(MediaDataContext context, IMountService mounts)
        {
            this.Context = context;
            this.Mounts = mounts;
        }

        protected MediaDataContext Context { get; }

        protected IMountService Mounts { get; }

        /// <inheritdoc />
        public async Task CreateFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            Debug.Assert(fileinfo.Directory != null, "Should never be null.");

            var mountpath = await this.Mounts.GetMountPath(mountPathId, cancellationToken);

            mountpath.Files.Add(fileinfo.CreateMountPathFile(mountPathId));

            await this.Context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            var mountpath = await this.GetMountPathWithFile(mountPathId, fileinfo, cancellationToken);
            var mountfile = mountpath.Files.Single(f => f.FilePath == fileinfo.DirectoryName && f.FileName == fileinfo.Name);

            mountpath.Files.Remove(mountfile);

            await this.Context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MountPathFile>> GetFiles(Guid mountPathId, CancellationToken cancellationToken)
        {
            return await this.Context.Mounts
                .SelectMany(m => m.MountPaths)
                .SelectMany(mp => mp.Files)
                .Where(mpf => mpf.MountPathId == mountPathId)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task RenameFile(Guid mountPathId, string filepath, string filename, FileInfo fileinfo,
            CancellationToken cancellationToken)
        {
            var mountpath = await this.GetMountPathWithFile(mountPathId, fileinfo, cancellationToken);
            var mountfile = mountpath.Files.Single(f => filepath == fileinfo.DirectoryName && f.FileName == filename);

            mountfile.SetFileInfo(fileinfo);

            await this.Context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            var mountpath = await this.GetMountPathWithFile(mountPathId, fileinfo, cancellationToken);
            var mountfile = mountpath.Files.Single(f => f.FilePath == fileinfo.DirectoryName && f.FileName == fileinfo.Name);

            mountfile.SetFileInfo(fileinfo);

            await this.Context.SaveChangesAsync(cancellationToken);
        }

        private Task<MountPath> GetMountPathWithFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            return this.Context.Mounts
                .Include(m => m.MountPaths)
                .ThenInclude(mp => mp.Files)
                .SelectMany(m => m.MountPaths)
                .SelectMany(mp => mp.Files)
                .Where(f => f.MountPathId == mountPathId)
                .Select(f => f.MountPath)
                .SingleAsync(mp => mp.Id == mountPathId, cancellationToken);
        }
    }
}
