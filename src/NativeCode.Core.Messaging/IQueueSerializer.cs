namespace NativeCode.Core.Messaging
{
    public interface IQueueSerializer
    {
        /// <summary>
        /// Deserialize the bytes into a JSON object. We assume the bytes are UTF-8
        /// and represents a JSON string.
        /// </summary>
        /// <remarks>
        /// NOTE: We want to encapsulate serializations so we can control the exceptions
        /// that flow from here.
        /// </remarks>
        /// <param name="data"></param>
        /// <returns></returns>
        T Deserialize<T>(byte[] data)
            where T : IQueueMessage;

        /// <summary>
        /// Serialize the incoming type to a UTF-8 JSON string and serialize
        /// it has a byte array.
        /// </summary>
        /// <remarks>
        /// NOTE: We want to encapsulate serializations so we can control the exceptions
        /// that flow from here.
        /// </remarks>
        /// <param name="instance"></param>
        /// <returns></returns>
        byte[] Serialize<T>(T instance)
            where T : IQueueMessage;
    }
}