namespace NativeCode.Clients.Radarr.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Serialization;
    using Requests;
    using Responses;
    using RestSharp;

    public class MovieResource : ResourceBase, IResourceLookup<int, Movie>
    {
        public MovieResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }

        public Task<IEnumerable<Movie>> All()
        {
            return this.GetCollection<Movie>("movie");
        }

        public Task<Movie> Get(int request)
        {
            return this.GetSingle<Movie>($"movie/{request}");
        }

        public Task<IResourcePage<Movie>> Page(int size, int start = 1)
        {
            throw new NotImplementedException();
        }

        public async Task PushRelease(ReleaseInfo release)
        {
            await this.Post("release/push", release);
        }
    }
}