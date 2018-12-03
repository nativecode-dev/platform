namespace NativeCode.Core.Serialization
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonObjectSerializer : IObjectSerializer
    {
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Culture = CultureInfo.CurrentCulture,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore
        };

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, this.settings);
        }

        public T DeserializeBytes<T>(byte[] value)
        {
            var json = Encoding.UTF8.GetString(value);
            return this.Deserialize<T>(json);
        }

        public T DeserializeStream<T>(Stream stream)
        {
            var serializer = JsonSerializer.Create(this.settings);

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }

        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, this.settings);
        }

        public byte[] SerializeBytes<T>(T value)
        {
            var json = this.Serialize(value);
            return Encoding.UTF8.GetBytes(json);
        }

        public void SerializeStream<T>(Stream stream, T value)
        {
            var serializer = JsonSerializer.Create(this.settings);

            using (var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}