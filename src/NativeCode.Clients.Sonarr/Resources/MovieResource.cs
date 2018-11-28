namespace NativeCode.Clients.Sonarr.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Serialization;
    using Requests;
    using Responses;
    using RestSharp;

    public class SeriesResource : ResourceBase, IResourceLookup<int, Series>
    {
        public SeriesResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }

        public Task<IEnumerable<Series>> All()
        {
            return this.GetCollection<Series>("series");
        }

        public Task<Series> Get(int request)
        {
            return this.GetSingle<Series>($"series/{request}");
        }

        public Task<IResourcePage<Series>> Page(int size, int start = 1)
        {
            throw new NotImplementedException();
        }

        public async Task PushRelease(ReleaseInfo release)
        {
            await this.Post("release/push", release);
        }
    }
}