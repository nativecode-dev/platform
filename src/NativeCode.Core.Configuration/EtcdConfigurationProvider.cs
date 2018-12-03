namespace NativeCode.Core.Configuration
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using dotnet_etcd;
    using Microsoft.Extensions.Configuration;
    using Nito.AsyncEx;

    public class EtcdConfigurationProvider : ConfigurationProvider
    {
        public EtcdConfigurationProvider(EtcdOptions options)
        {
            this.Options = options;
        }

        protected EtcdOptions Options { get; }

        public override void Load()
        {
            AsyncContext.Run(this.LoadAsync);
        }

        protected virtual async Task LoadAsync()
        {
            using (var client = this.CreateClient())
            {
                var path = this.Options.HostUri.AbsolutePath;
                var response = await client.GetRangeAsync(path);

                Trace.WriteLine(path);

                foreach (var kvp in response)
                {
                    if (kvp.Key == path)
                    {
                        continue;
                    }

                    var key = kvp.Key.Substring(path.Length + 1)
                        .Replace("/", ":");
                    this.Data.Add(key, kvp.Value);
                }
            }
        }

        private EtcdClient CreateClient()
        {
            return new EtcdClient(this.Options.HostUri.Host, this.Options.HostUri.Port);
        }
    }
}