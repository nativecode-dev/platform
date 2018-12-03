namespace NativeCode.Clients.Radarr.Resources
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Serialization;
    using Responses;
    using RestSharp;

    public class DiskSpaceResource : ResourceBase, IResourceLookup<IEnumerable<DiskSpace>>
    {
        public DiskSpaceResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }

        public Task<IEnumerable<DiskSpace>> Get()
        {
            return this.GetCollection<DiskSpace>("diskspace");
        }
    }
}