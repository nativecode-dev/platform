namespace NativeCode.Clients.Sonarr.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Clients.Sonarr.Requests;
    using NativeCode.Clients.Sonarr.Responses;
    using NativeCode.Core.Serialization;

    using RestSharp;

    public class MovieResource : ResourceBase, IResourceLookup<int, Series>
    {
        public MovieResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }

        public Task<IEnumerable<Series>> All()
        {
            return this.GetCollection<Series>("series");
        }

        public Task<Series> GetResource(int request)
        {
            return this.GetSingle<Series>($"series/{request}");
        }

        public Task<IResourcePage<Series>> Page(int size, int start = 1)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PushRelease(SeriesReleaseInfo release)
        {
            return this.Post("release/push", release);
        }
    }
}
