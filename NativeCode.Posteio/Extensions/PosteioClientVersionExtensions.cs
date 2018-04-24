namespace NativeCode.Posteio.Extensions
{
    public static class PosteioClientVersionExtensions
    {
        public static string AsPathString(this ClientVersion version)
        {
            switch (version)
            {
                case ClientVersion.V2: return "v2";
                default: return "v1";
            }
        }
    }
}
