namespace NativeCode.Core.Extensions
{
    using System;
    using System.Collections.Generic;
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

        public static IDictionary<string, string> ParseQueryParams(this Uri uri)
        {
            var results = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(uri.Query))
            {
                return results;
            }

            var query = uri.Query.Split('?');
            var querystr = query[query.Length - 1];
            var parts = querystr.Split('&');

            foreach (var part in parts)
            {
                var kvp = part.Split('=');
                results.Add(kvp[0], kvp.Length > 1 ? kvp[1] : null);
            }

            return results;
        }
    }
}