namespace NativeCode.Core.Serialization
{
    using System.IO;

    public interface IObjectSerializer
    {
        T Deserialize<T>(string value);

        T DeserializeBytes<T>(byte[] value);

        T DeserializeStream<T>(Stream stream);

        string Serialize<T>(T value);

        byte[] SerializeBytes<T>(T value);

        void SerializeStream<T>(Stream stream, T value);
    }
}