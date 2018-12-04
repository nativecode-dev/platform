namespace NativeCode.Core.Extensions
{
    using System.Text;
    using Newtonsoft.Json;

    public static class ObjectExtensions
    {
        public static T FromJsonBytes<T>(this byte[] bytes, Encoding encoding = null, JsonSerializerSettings settings = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            settings = settings ?? new JsonSerializerSettings();

            var json = encoding.GetString(bytes);

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static byte[] ToJsonBytes<T>(this T instance, Encoding encoding = null, JsonSerializerSettings settings = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            settings = settings ?? new JsonSerializerSettings();

            var json = JsonConvert.SerializeObject(instance, settings);

            return encoding.GetBytes(json);
        }
    }
}
