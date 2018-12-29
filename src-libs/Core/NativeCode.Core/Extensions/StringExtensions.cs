namespace NativeCode.Core.Extensions
{
    using System.Globalization;
    using System.Text;

    public static class StringExtensions
    {
        public static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(
                    b.ToString("x2", CultureInfo.CurrentCulture)
                        .ToUpperInvariant());
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
