namespace NativeCode.Core.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Reliability;

    public abstract class HostedService : Disposable, IHostedService
    {
        public abstract Task StartAsync(CancellationToken cancellationToken);

        public abstract Task StopAsync(CancellationToken cancellationToken);
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
