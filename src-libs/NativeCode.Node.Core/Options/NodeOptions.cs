namespace NativeCode.Node.Core.Options
{
    public class NodeOptions
    {
        public string Authority { get; set; } = "https://idp.nativecode.com";

        public string Name { get; set; }

        public string RedisHost { get; set; } = "redis";
    }
}
