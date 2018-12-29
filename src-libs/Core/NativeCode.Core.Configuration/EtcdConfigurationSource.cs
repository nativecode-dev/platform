namespace NativeCode.Core.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class EtcdConfigurationSource : IConfigurationSource
    {
        public EtcdConfigurationSource(EtcdOptions options)
        {
            this.Options = options;
        }

        protected EtcdOptions Options { get; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EtcdConfigurationProvider(this.Options);
        }
    }
}
