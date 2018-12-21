namespace NativeCode.Node.Core.Options
{
    using System;

    public class NodeOptions
    {
        public string ApiName { get; set; } = "node";

        public string ApiScope { get; set; } = "api.node";

        public string ApiSecret { get; set; } = "lLo3zoT@XZr5O08L7xS*trJu";

        public string ClientId { get; set; } = "nativecode";

        public string ClientSecret { get; set; } = "&mB2gGm@S1H1gxezkikNf&$n";

        public TimeSpan? ClockSkew { get; set; }

        public string Authority { get; set; } = "https://localhost:5000";

        public string Name { get; set; }

        public string RedisHost { get; set; } = "redis";
    }
}
