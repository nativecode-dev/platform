namespace NativeCode.Core.Extensions
{
    using System;

    using JetBrains.Annotations;

    public static class UriExtensions
    {
        public static void AddQueryParam(this UriBuilder builder, [NotNull] string name, [NotNull] string value)
        {
            if (string.IsNullOrWhiteSpace(builder.Query))
            {
                builder.Query = $"?{name}={value}";
                return;
            }

            builder.Query = $"&{name}={value}";
        }

        public static Uri AddQueryParam(this Uri uri, [NotNull] string name, [NotNull] string value)
        {
            var builder = new UriBuilder(uri);
            builder.AddQueryParam(name, value);

            return builder.Uri;
        }
    }
}
