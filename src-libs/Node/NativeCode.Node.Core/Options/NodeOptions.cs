namespace NativeCode.Node.Core.Options
{
    using System;

    public class NodeOptions
    {
        public string ApiName { get; set; } = "node";

        public string ApiScope { get; set; } = "api.node";

        public string ApiSecret { get; set; } = "dev-2018-NATIVECODE";

        public string Authority { get; set; } = "https://localhost:5000";

        public string ClientId { get; set; } = "nativecode";

        public string ClientSecret { get; set; } = "dev-2018-NATIVECODE";

        public TimeSpan? ClockSkew { get; set; }

        public string Name { get; set; }
    }
}
