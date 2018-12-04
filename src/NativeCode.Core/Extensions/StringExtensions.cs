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
    }
}
