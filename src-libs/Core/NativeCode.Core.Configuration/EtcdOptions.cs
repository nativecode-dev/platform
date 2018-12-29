namespace NativeCode.Core.Configuration
{
    using System;

    public class EtcdOptions
    {
        public string Host { get; set; } = "https://etcd:2379";

        public Uri HostUri => new Uri(this.Host);
    }
}
