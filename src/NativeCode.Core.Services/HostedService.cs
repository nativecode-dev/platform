namespace NativeCode.Core.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Reliability;

    public abstract class HostedService : Disposable, IHostedService
    {
        public HostServiceState State { get; private set; }

        protected Task Current { get; private set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (this.State == HostServiceState.Running)
            {
                return this.Current;
            }

            this.State = HostServiceState.Running;
            return this.Current = this.DoStartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (this.State == HostServiceState.Running)
            {
                return this.DoStopAsync(cancellationToken);
            }

            return Task.CompletedTask;
        }

        protected abstract Task DoStartAsync(CancellationToken cancellationToken);

        protected abstract Task DoStopAsync(CancellationToken cancellationToken);
    }

    public abstract class HostedService<TOptions> : HostedService
        where TOptions : class, new()
    {
        protected HostedService(IOptions<TOptions> options)
        {
            this.Options = options.Value;
        }

        protected TOptions Options { get; }
    }
}
