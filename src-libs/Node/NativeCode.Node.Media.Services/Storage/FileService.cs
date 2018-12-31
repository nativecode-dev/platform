namespace NativeCode.Node.Media.Services.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Data.Data.Storage;
    using Data.Extensions;
    using Data.Services.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Extensions;

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
                .NoCapture();
            var mountfile = fileinfo.CreateMountPathFile(mountPathId);

            mountpath.Files.Add(mountfile);

            await this.Context.SaveChangesAsync(cancellationToken)
                .NoCapture();
        }

        /// <inheritdoc />
        public async Task DeleteFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            await this.Context.SaveChangesAsync(cancellationToken)
                .NoCapture();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MountPathFile>> GetFiles(Guid mountPathId, CancellationToken cancellationToken)
        {
            return await this.Context.Mounts.SelectMany(m => m.Paths)
                .SelectMany(mp => mp.Files)
                .Where(mpf => mpf.MountPathId == mountPathId)
                .ToListAsync(cancellationToken)
                .NoCapture();
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
                .NoCapture();

            mountfile.SetFileInfo(fileinfo);

            await this.Context.SaveChangesAsync(cancellationToken)
                .NoCapture();
        }

        /// <inheritdoc />
        public async Task UpdateFile(Guid mountPathId, FileInfo fileinfo, CancellationToken cancellationToken)
        {
            var mountfile = await this.GetMountPathFile(mountPathId, fileinfo.Name, cancellationToken)
                .NoCapture();

            if (mountfile == null)
            {
                await this.CreateFile(mountPathId, fileinfo, cancellationToken)
                    .NoCapture();
                return;
            }

            mountfile.SetFileInfo(fileinfo);

            await this.Context.SaveChangesAsync(cancellationToken)
                .NoCapture();
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
