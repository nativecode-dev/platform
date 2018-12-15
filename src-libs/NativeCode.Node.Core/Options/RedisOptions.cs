namespace NativeCode.Node.Core.Options
{
    public class RedisOptions
    {
        public int RedisOperationalStore { get; set; } = 2;

        public string RedisOperationalStoreKey { get; set; } = "OperationalStore";

        public string RedisConnection { get; set; } = "redis";
    }
}