namespace NativeCode.Clients
{
    using Core.Serialization;
    using RestSharp.Serializers;

    public class RestSerializer : ISerializer
    {
        private readonly IObjectSerializer serializer;

        public RestSerializer(IObjectSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string ContentType { get; set; } = "application/json";

        public string Serialize(object obj)
        {
            return this.serializer.Serialize(obj);
        }
    }
}