namespace NativeCode.Core.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class HostedTimerService : HostedService
    {
        private readonly Timer timer;

        protected HostedTimerService()
        {
            this.timer = new Timer(this.OnTick, null, Timeout.InfiniteTimeSpan, TimeSpan.Zero);
        }

        public TimeSpan Duration { get; set; } = TimeSpan.Zero;

        public TimeSpan Period { get; set; } = TimeSpan.FromSeconds(10);

        protected override Task DoStartAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(this.Duration, this.Period);

            return Task.CompletedTask;
        }

        protected override Task DoStopAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(Timeout.InfiniteTimeSpan, TimeSpan.Zero);

            return Task.CompletedTask;
        }

        protected override void ReleaseManaged()
        {
            this.timer.Dispose();
        }

        protected abstract void OnTick(object state);
    }
}
