namespace NativeCode.Node.Core.Options
{
    public class RedisOptions
    {
        public string Host { get; set; } = "redis";

        public int RedisOperationalStore { get; set; } = 1;

        public string RedisOperationalStoreKey { get; set; } = "OperationalStore";
    }
}
