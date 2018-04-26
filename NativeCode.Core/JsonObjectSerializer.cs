namespace NativeCode.Core
{
    using System.IO;
    using System.Text;

    using Newtonsoft.Json;

    public class JsonObjectSerializer : IObjectSerializer
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public T DeserializeBytes<T>(byte[] value)
        {
            var json = Encoding.UTF8.GetString(value);
            return this.Deserialize<T>(json);
        }

        public T DeserializeStream<T>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }

        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public byte[] SerializeBytes<T>(T value)
        {
            var json = this.Serialize(value);
            return Encoding.UTF8.GetBytes(json);
        }

        public void SerializeStream<T>(Stream stream, T value)
        {
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
