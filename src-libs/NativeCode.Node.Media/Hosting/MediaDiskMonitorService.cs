namespace NativeCode.Node.Media.Hosting
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using NativeCode.Core.Services;
    using Nito.AsyncEx;
    using Services;

    public class MediaDiskMonitorService : HostedService<MediaDiskMonitorOptions>
    {
        /// <inheritdoc />
        public MediaDiskMonitorService(
            IOptions<MediaDiskMonitorOptions> options,
            ILogger<MediaDiskMonitorService> logger,
            IFileMonitorService fileMonitors,
            IMountService mountService) : base(options)
        {
            this.FileMonitors = fileMonitors;
            this.Logger = logger;
            this.MountService = mountService;
        }

        protected IFileMonitorService FileMonitors { get; }

        protected ILogger<MediaDiskMonitorService> Logger { get; }

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

            foreach (var mount in this.Options.MountPaths)
            {
                var path = await this.MountService.GetMountPath(mount.MountPathId, cancellationToken);
                this.FileMonitors.StartMonitor(path);
            }
        }

        /// <inheritdoc />
        protected override async Task Stop(CancellationToken cancellationToken)
        {
            if (this.Options.Enabled == false)
            {
                return;
            }

            foreach (var mount in this.Options.MountPaths)
            {
                var path = await this.MountService.GetMountPath(mount.MountPathId, cancellationToken);
                this.FileMonitors.StopMonitor(path);
            }
        }
    }
}
