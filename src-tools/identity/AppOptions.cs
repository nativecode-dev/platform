namespace identity
{
    using NativeCode.Node.Core.Options;

    public class AppOptions : NodeOptions
    {
        public AppOptions()
        {
            this.ClientId = "nativecode-mvc";
            this.ClientSecret = "dev-2018-NATIVECODE-MVC";
        }

        public bool RequireHttpsMetadata { get; set; }
    }
}
