namespace NativeCode.Node.Media.Services.Storage
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

    public class FileService : IFileService
    {
        public FileService(MediaDataContext context, IMountService mounts)
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

            var mountpath = await this.Mounts.GetMountPathById(mountPathId, cancellationToken);
            var mountfile = fileinfo.CreateMountPathFile(mountPathId);

            mountpath.Files.Add(mountfile);

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
        public Task<IEnumerable<MountPathFile>> ImportFiles(MountPath mount, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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
            var mountfile = mountpath.Files.SingleOrDefault(f => f.FilePath == fileinfo.DirectoryName && f.FileName == fileinfo.Name);

            if (mountfile == null)
            {
                await this.CreateFile(mountPathId, fileinfo, cancellationToken);
                return;
            }

            mountfile.SetFileInfo(fileinfo);

            await this.Context.SaveChangesAsync(cancellationToken);
        }

        private async Task<MountPath> GetMountPathWithFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            var path = await this.Context.Mounts
                .SelectMany(m => m.MountPaths)
                .Include(mp => mp.Files)
                .Include(mp => mp.Mount)
                .SelectMany(mp => mp.Files)
                .Select(f => f.MountPath)
                .SingleOrDefaultAsync(mp => mp.Id == mountPathId, cancellationToken);

            if (path != null)
            {
                return path;
            }

            return await this.Context.Mounts.SelectMany(m => m.MountPaths)
                .SingleAsync(mp => mp.Id == mountPathId, cancellationToken);
        }
    }
}
