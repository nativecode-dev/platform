namespace NativeCode.Clients.Sonarr.Resources
{
    using NativeCode.Core.Serialization;

    using RestSharp;

    public class CalendarResource : ResourceBase
    {
        public CalendarResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }
    }
}
