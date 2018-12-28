namespace NativeCode.Node.Media.Services.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using NativeCode.Node.Media.Data;
    using NativeCode.Node.Media.Data.Data.Storage;
    using NativeCode.Node.Media.Data.Extensions;
    using NativeCode.Node.Media.Data.Services.Storage;

    public class FileService : IFileService
    {
        public FileService(MediaDataContext context, ILogger<FileService> logger, IMountService mounts)
        {
            this.Context = context;
            this.Logger = logger;
            this.Mounts = mounts;
        }

        protected MediaDataContext Context { get; }

        protected ILogger<FileService> Logger { get; }

        protected IMountService Mounts { get; }

        /// <inheritdoc />
        public async Task CreateFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            Debug.Assert(fileinfo.Directory != null, "Should never be null.");

            var mountpath = await this.Mounts.GetMountPathById(mountPathId, cancellationToken)
                                .ConfigureAwait(false);
            var mountfile = fileinfo.CreateMountPathFile(mountPathId);

            mountpath.Files.Add(mountfile);

            await this.Context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            await this.Context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MountPathFile>> GetFiles(Guid mountPathId, CancellationToken cancellationToken)
        {
            return await this.Context.Mounts.SelectMany(m => m.Paths)
                       .SelectMany(mp => mp.Files)
                       .Where(mpf => mpf.MountPathId == mountPathId)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<IEnumerable<MountPathFile>> ImportFiles(MountPath mount, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task RenameFile(
            Guid mountPathId,
            string filepath,
            string filename,
            FileInfo fileinfo,
            CancellationToken cancellationToken)
        {
            var mountfile = await this.GetMountPathFile(mountPathId, filename, cancellationToken)
                                .ConfigureAwait(false);

            mountfile.SetFileInfo(fileinfo);

            await this.Context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task UpdateFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            var mountfile = await this.GetMountPathFile(mountPathId, fileinfo.Name, cancellationToken)
                                .ConfigureAwait(false);

            if (mountfile == null)
            {
                await this.CreateFile(mountPathId, fileinfo, cancellationToken)
                    .ConfigureAwait(false);
                return;
            }

            mountfile.SetFileInfo(fileinfo);

            await this.Context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        private Task<MountPathFile> GetMountPathFile(Guid mountPathId, string filename, CancellationToken cancellationToken)
        {
            return this.Context.Mounts.Include(m => m.Paths)
                .ThenInclude(mp => mp.Files)
                .SelectMany(m => m.Paths)
                .SelectMany(mp => mp.Files)
                .Where(mpf => mpf.MountPathId == mountPathId)
                .SingleOrDefaultAsync(mpf => mpf.FileName == filename, cancellationToken);
        }
    }
}
