namespace NativeCode.Core
{
    public static class KeyValueServerConfig
    {
        public static (string, string, string) Standard(string owner, string product, string environment)
        {
            return (
                "tcp://etcd:2379/root/Global",
                $"tcp://etcd:2379/root/{owner}/Common",
                $"tcp://etcd:2379/root/{owner}/{product}/{environment}"
            );
        }
    }
}
