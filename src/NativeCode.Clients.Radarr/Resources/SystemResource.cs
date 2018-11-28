namespace NativeCode.Clients.Radarr.Resources
{
    using System.Threading.Tasks;
    using Core.Serialization;
    using Responses;
    using RestSharp;

    public class SystemResource : ResourceBase, IResourceLookup<SystemStatus>
    {
        public SystemResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }

        public Task<SystemStatus> Get()
        {
            return this.GetSingle<SystemStatus>("system/status");
        }
    }
}