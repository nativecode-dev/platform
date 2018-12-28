namespace NativeCode.Node.Media.Services.Storage
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    using Data.Data.Storage;
    using Data.Extensions;
    using Data.Services.Storage;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Reliability;
    using Nito.AsyncEx;

    public class MonitorService : IMonitorService
    {
        private readonly ConcurrentDictionary<Guid, FileSystemWatcherProxy> monitors =
            new ConcurrentDictionary<Guid, FileSystemWatcherProxy>();

        public MonitorService(ILogger<MonitorService> logger, IFileService fileService)
        {
            this.FileService = fileService;
            this.Logger = logger;
        }

        protected IFileService FileService { get; }

        protected ILogger<MonitorService> Logger { get; }

        /// <inheritdoc />
        public Task StartMonitor(MountPath mountPath)
        {
            if (this.monitors.ContainsKey(mountPath.Id))
            {
                return Task.CompletedTask;
            }

            var proxy = new FileSystemWatcherProxy(this.FileService, this.Logger, mountPath);

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
                await proxy.Stop()
                    .ConfigureAwait(false);

                proxy.Dispose();
            }
        }

        protected sealed class FileSystemWatcherProxy : Disposable
        {
            public FileSystemWatcherProxy(IFileService fileService, ILogger<MonitorService> logger, MountPath mountPath)
            {
                this.FileService = fileService;
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

            private Task CurrentTask { get; set; }

            private IFileService FileService { get; }

            private ILogger<MonitorService> Logger { get; }

            private MountPath MountPath { get; }

            private FileSystemWatcher Monitor { get; }

            public Task Start()
            {
                if (this.CurrentTask != null)
                {
                    return this.CurrentTask;
                }

                return this.CurrentTask = Task.Factory.Run(() => this.Monitor.EnableRaisingEvents = true);
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
                    AsyncContext.Run(() => this.FileService.UpdateFile(this.MountPath.Id, fileinfo));
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
                    AsyncContext.Run(() => this.FileService.CreateFile(this.MountPath.Id, fileinfo));
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
                    AsyncContext.Run(() => this.FileService.DeleteFile(this.MountPath.Id, fileinfo));
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
                    AsyncContext.Run(() => this.FileService.RenameFile(this.MountPath.Id, e.OldFullPath, e.OldName, fileinfo));
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
