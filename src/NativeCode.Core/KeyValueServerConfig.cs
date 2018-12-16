namespace NativeCode.Core
{
    using System;

    public static class KeyValueServerConfig
    {
        public static string[] Standard(string owner, string product, string environment)
        {
            return new[]
            {
                "tcp://etcd:2379/root/Global",
                $"tcp://etcd:2379/root/{owner}/Common",
                $"tcp://etcd:2379/root/{owner}/{product}/{environment}",
                $"tcp://etcd:2379/root/{owner}/{product}/{Environment.MachineName}",
            };
        }
    }
}
