namespace identity
{
    using System;

    public class AppOptions
    {
        public string Authority { get; set; } = "https://localhost:5000";

        public string ApiName { get; set; } = "identity";

        public string ApiScope { get; set; } = "api.identity";

        public string ApiSecret { get; set; }

        public string ClientId { get; set; } = "nativecode";

        public string ClientSecret { get; set; }

        public TimeSpan ClockSkew { get; set; } = TimeSpan.FromMinutes(5);

        public bool RequireHttpsMetadata { get; set; }
    }
}
