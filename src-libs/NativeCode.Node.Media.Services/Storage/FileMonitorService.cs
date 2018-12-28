namespace NativeCode.Node.Media.Services.Storage
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    using Data.Storage;
    using Extensions;
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

        protected IFileStorageService FileStorage { get; }

        protected ILogger<FileMonitorService> Logger { get; }

        /// <inheritdoc />
        public Task StartMonitor(MountPath mountPath)
        {
            if (this.monitors.ContainsKey(mountPath.Id))
            {
                return Task.CompletedTask;
            }

            var proxy = new FileSystemWatcherProxy(this.FileStorage, this.Logger, mountPath);

            if (this.monitors.TryAdd(mountPath.Id, proxy) == false)
            {
                throw new InvalidOperationException($"Failed to monitor mount: {mountPath.Id}");
            }

            return proxy.Start();
        }

        /// <inheritdoc />
        public async Task StopMonitor(MountPath mountPath)
        {
            if (this.monitors.ContainsKey(mountPath.Id) && this.monitors.TryRemove(mountPath.MountId, out var proxy))
            {
                await proxy.Stop();

                proxy.Dispose();
            }
        }

        protected class FileSystemWatcherProxy : Disposable
        {
            public FileSystemWatcherProxy(IFileStorageService fileStorageService, ILogger<FileMonitorService> logger, MountPath mountPath)
            {
                this.FileStorage = fileStorageService;
                this.Logger = logger;
                this.MountPath = mountPath;

                var uri = this.MountPath.GetMountUri();

                this.Monitor = new FileSystemWatcher
                {
                    IncludeSubdirectories = true,
                    Path = uri.LocalPath,
                };

                this.Monitor.Changed += this.MonitorOnChanged;
                this.Monitor.Created += this.MonitorOnCreated;
                this.Monitor.Deleted += this.MonitorOnDeleted;
                this.Monitor.Renamed += this.MonitorOnRenamed;
            }

            protected IFileStorageService FileStorage { get; }

            protected ILogger<FileMonitorService> Logger { get; }

            protected MountPath MountPath { get; }

            protected Task CurrentTask { get; private set; }

            private FileSystemWatcher Monitor { get; }

            public Task Start()
            {
                if (this.CurrentTask != null)
                {
                    return this.CurrentTask;
                }

                return this.CurrentTask = Task.Factory.StartNew(() => this.Monitor.EnableRaisingEvents = true);
            }

            public Task Stop()
            {
                if (this.CurrentTask != null)
                {
                    this.Monitor.EnableRaisingEvents = false;

                    return this.CurrentTask;
                }

                return Task.CompletedTask;
            }

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
                    var fileinfo = new FileInfo(e.FullPath);
                    AsyncContext.Run(() => this.FileStorage.UpdateFile(this.MountPath.Id, fileinfo));
                }
                catch (IOException ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
            }

            private void MonitorOnCreated(object sender, FileSystemEventArgs e)
            {
                try
                {
                    var fileinfo = new FileInfo(e.FullPath);
                    AsyncContext.Run(() => this.FileStorage.CreateFile(this.MountPath.Id, fileinfo));
                }
                catch (IOException ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
            }

            private void MonitorOnDeleted(object sender, FileSystemEventArgs e)
            {
                try
                {
                    var fileinfo = new FileInfo(e.FullPath);
                    AsyncContext.Run(() => this.FileStorage.DeleteFile(this.MountPath.Id, fileinfo));
                }
                catch (IOException ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
            }

            private void MonitorOnRenamed(object sender, RenamedEventArgs e)
            {
                try
                {
                    var fileinfo = new FileInfo(e.FullPath);
                    AsyncContext.Run(() => this.FileStorage.RenameFile(this.MountPath.Id, e.OldFullPath, e.OldName, fileinfo));
                }
                catch (IOException ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError("{@ex}", ex);
                }
            }
        }
    }
}
