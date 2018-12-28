namespace NativeCode.Node.Media.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using NativeCode.Core.Services;
    using Nito.AsyncEx;
    using Services.Storage;

    public class StorageMonitorService : HostedService<StorageMonitorOptions>
    {
        /// <inheritdoc />
        public StorageMonitorService(
            IOptions<StorageMonitorOptions> options,
            ILogger<StorageMonitorService> logger,
            IFileMonitorService fileMonitors,
            IMountService mountService) : base(options)
        {
            this.FileMonitors = fileMonitors;
            this.Logger = logger;
            this.MountService = mountService;
        }

        protected IFileMonitorService FileMonitors { get; }

        protected ILogger<StorageMonitorService> Logger { get; }

        protected IMountService MountService { get; }

        /// <inheritdoc />
        protected override void ReleaseManaged()
        {
            AsyncContext.Run(() => this.Stop(CancellationToken.None));
        }

        /// <inheritdoc />
        protected override async Task Start(CancellationToken cancellationToken)
        {
            if (this.Options.Enabled == false)
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var mount in this.Options.Mounts)
            {
                var path = await this.MountService.GetMountPath(Guid.Parse(mount.Key), mount.Value, cancellationToken);
                tasks.Add(this.FileMonitors.StartMonitor(path));
            }

            await Task.WhenAll(tasks);
        }

        /// <inheritdoc />
        protected override async Task Stop(CancellationToken cancellationToken)
        {
            if (this.Options.Enabled == false)
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var mount in this.Options.Mounts)
            {
                var path = await this.MountService.GetMountPath(Guid.Parse(mount.Key), mount.Value, cancellationToken);
                tasks.Add(this.FileMonitors.StopMonitor(path));
            }

            await Task.WhenAll(tasks);
        }
    }
}
