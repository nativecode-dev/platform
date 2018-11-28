namespace NativeCode.Core.Messaging
{
    using System;
    using System.Text;
    using Exceptions;
    using Newtonsoft.Json;

    public class QueueSerializer : IQueueSerializer
    {
        public T Deserialize<T>(byte[] data)
            where T : IQueueMessage
        {
            try
            {
                var json = Encoding.UTF8.GetString(data);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new QueueDeserializationException<T>(ex);
            }
        }

        public byte[] Serialize<T>(T instance)
            where T : IQueueMessage
        {
            try
            {
                var json = JsonConvert.SerializeObject(instance);
                return Encoding.UTF8.GetBytes(json);
            }
            catch (Exception ex)
            {
                throw new QueueSerializationException<T>(ex);
            }
        }
    }
}