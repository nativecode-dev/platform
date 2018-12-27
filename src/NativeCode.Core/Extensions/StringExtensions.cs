namespace NativeCode.Core.Extensions
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class StringExtensions
    {
        public static Guid GetGuid(this string value)
        {
            using (var provider = new MD5CryptoServiceProvider())
            {
                var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(value));
                return new Guid(hash);
            }
        }

        public static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2")
                    .ToLower());
            }

            return sb.ToString();
        }

        public static string ToSecretString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                return value;
            }

            return $"*****{value.Substring(value.Length - 5, 5)}";
        }
    }
}
