namespace NativeCode.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Core;

    using RestSharp;

    public abstract class ResourceBase<TRequest, TResponse> : IResource<TRequest, TResponse>
    {
        protected ResourceBase(IRestClient client, IObjectSerializer serializer)
        {
            this.Client = client;
            this.Serializer = serializer;
        }

        protected IRestClient Client { get; }

        protected IObjectSerializer Serializer { get; }

        public abstract Task<bool> Add(TRequest request);

        public abstract Task<bool> Add(IEnumerable<TRequest> requests);

        public abstract Task<IEnumerable<TResponse>> All();

        public abstract Task<IResourcePage<TResponse>> Page(int size, int start = 1);

        public abstract Task<IEnumerable<TResponse>> Query(TRequest request);

        public abstract Task<IResourcePage<TResponse>> QueryPage(TRequest request, int size, int start = 1);

        public abstract Task<bool> Remove(TRequest request);

        public abstract Task<bool> Remove(IEnumerable<TRequest> requests);

        protected virtual async Task<bool> Delete(string path)
        {
            var req = new RestRequest(path, Method.DELETE);
            var response = await this.Client.ExecuteTaskAsync(req);

            return response.IsSuccessful;
        }

        protected virtual async Task<TResponse> Get(string path)
        {
            var req = new RestRequest(path, Method.GET);
            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<TResponse>(response.Content);
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<IEnumerable<TResponse>> GetCollection(string path)
        {
            var req = new RestRequest(path, Method.GET);
            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<IEnumerable<TResponse>>(response.Content);
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<IResourcePage<TResponse>> GetCollectionPage(string path)
        {
            var req = new RestRequest(path, Method.GET);
            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<IResourcePage<TResponse>>(response.Content);
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<bool> Post(string path, TRequest request)
        {
            var req = new RestRequest(path, Method.POST);
            req.AddJsonBody(request);

            var response = await this.Client.ExecuteTaskAsync(req);
            return response.IsSuccessful;
        }

        protected virtual async Task<TResponse> PostResponse(string path, TRequest request)
        {
            var req = new RestRequest(path, Method.POST);
            req.AddJsonBody(request);

            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<TResponse>(response.Content);
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<bool> Put(string path, TRequest request)
        {
            var req = new RestRequest(path, Method.PUT);
            req.AddJsonBody(request);

            var response = await this.Client.ExecuteTaskAsync(req);
            return response.IsSuccessful;
        }

        protected virtual async Task<TResponse> PutResponse(string path, TRequest request)
        {
            var req = new RestRequest(path, Method.PUT);
            req.AddJsonBody(request);

            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<TResponse>(response.Content);
            }

            throw new InvalidOperationException();
        }
    }
}
