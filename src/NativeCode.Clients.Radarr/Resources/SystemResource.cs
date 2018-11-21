namespace NativeCode.Clients.Radarr.Resources
{
    using System.Threading.Tasks;

    using NativeCode.Clients.Radarr.Responses;
    using NativeCode.Core;

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
