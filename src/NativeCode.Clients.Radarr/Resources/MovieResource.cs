namespace NativeCode.Clients.Radarr.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Clients.Radarr.Requests;
    using NativeCode.Clients.Radarr.Responses;
    using NativeCode.Core.Serialization;

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

        public Task<Movie> GetResource(int request)
        {
            return this.GetSingle<Movie>($"movie/{request}");
        }

        public Task<IResourcePage<Movie>> Page(int size, int start = 1)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PushRelease(MovieReleaseInfo release)
        {
            return this.Post("release/push", release);
        }
    }
}
