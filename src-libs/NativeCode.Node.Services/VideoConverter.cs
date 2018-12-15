namespace NativeCode.Node.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Services;
    using Microsoft.Extensions.Options;

    public class VideoConverter : HostedService<VideoConverterOptions>
    {
        public VideoConverter(IOptions<VideoConverterOptions> options) : base(options)
        {
        }

        protected override void ReleaseManaged()
        {
            throw new NotImplementedException();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
