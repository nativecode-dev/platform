namespace NativeCode.Node.Media.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using Data.Storage;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Reliability;
    using Nito.AsyncEx;

    public class FileMonitorService : IFileMonitorService
    {
        private readonly ConcurrentDictionary<Guid, FileSystemWatcherProxy> monitors =
            new ConcurrentDictionary<Guid, FileSystemWatcherProxy>();

        public FileMonitorService(ILogger<FileMonitorService> logger, IFileStorageService fileStorage)
        {
            this.FileStorage = fileStorage;
            this.Logger = logger;
        }

        protected ILogger<FileMonitorService> Logger { get; }

        protected FileSystemWatcher Monitor { get; private set; }

        protected IFileStorageService FileStorage { get; }

        protected MountPath MountPath { get; private set; }

        /// <inheritdoc />
        public void StartMonitor(MountPath mountPath)
        {
            if (this.monitors.ContainsKey(mountPath.Id))
            {
                return;
            }

            if (this.monitors.TryAdd(mountPath.Id, new FileSystemWatcherProxy(this.FileStorage, this.Logger, mountPath)) == false)
            {
                throw new InvalidOperationException($"Failed to monitor mount: {mountPath.Id}");
            }
        }

        /// <inheritdoc />
        public void StopMonitor(MountPath mountPath)
        {
            if (this.monitors.ContainsKey(mountPath.Id) && this.monitors.TryRemove(mountPath.MountId, out var proxy))
            {
                proxy.Dispose();
            }
        }

        protected class FileSystemWatcherProxy : Disposable
        {
            public FileSystemWatcherProxy(IFileStorageService fileStorageService, ILogger<FileMonitorService> logger, MountPath mountPath)
            {
                this.FileStorage = fileStorageService;
                this.Logger = logger;

                this.Monitor = new FileSystemWatcher
                {
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = true,
                    Path = this.MountPath.Path,
                };

                this.Monitor.Changed += this.MonitorOnChanged;
                this.Monitor.Created += this.MonitorOnCreated;
                this.Monitor.Deleted += this.MonitorOnDeleted;
                this.Monitor.Renamed += this.MonitorOnRenamed;
                this.MountPath = mountPath;
            }

            protected MountPath MountPath { get; }

            protected ILogger<FileMonitorService> Logger { get; }

            protected IFileStorageService FileStorage { get; }

            private FileSystemWatcher Monitor { get; }

            /// <inheritdoc />
            protected override void ReleaseManaged()
            {
                this.Monitor.Renamed -= this.MonitorOnRenamed;
                this.Monitor.Deleted -= this.MonitorOnDeleted;
                this.Monitor.Created -= this.MonitorOnCreated;
                this.Monitor.Changed -= this.MonitorOnChanged;
                this.Monitor.Dispose();
            }

            private void MonitorOnChanged(object sender, FileSystemEventArgs e)
            {
                try
                {
                    var fileinfo = new FileInfo(Path.Combine(e.FullPath, e.Name));
                    AsyncContext.Run(() => this.FileStorage.UpdateFile(this.MountPath.Id, fileinfo));
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                    throw;
                }
            }

            private void MonitorOnCreated(object sender, FileSystemEventArgs e)
            {
                try
                {
                    var fileinfo = new FileInfo(Path.Combine(e.FullPath, e.Name));
                    AsyncContext.Run(() => this.FileStorage.CreateFile(this.MountPath.Id, fileinfo));
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                    throw;
                }
            }

            private void MonitorOnDeleted(object sender, FileSystemEventArgs e)
            {
                try
                {
                    var fileinfo = new FileInfo(Path.Combine(e.FullPath, e.Name));
                    AsyncContext.Run(() => this.FileStorage.DeleteFile(this.MountPath.Id, fileinfo));
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                    throw;
                }
            }

            private void MonitorOnRenamed(object sender, RenamedEventArgs e)
            {
                try
                {
                    var fileinfo = new FileInfo(Path.Combine(e.FullPath, e.Name));
                    AsyncContext.Run(() => this.FileStorage.RenameFile(this.MountPath.Id, e.OldFullPath, e.OldName, fileinfo));
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                    throw;
                }
            }
        }
    }
}
