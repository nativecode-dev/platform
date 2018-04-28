namespace NativeCode.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Core;

    using RestSharp;

    public abstract class ResourceBase : IResource
    {
        protected ResourceBase(IRestClient client, IObjectSerializer serializer)
        {
            this.Client = client;
            this.Serializer = serializer;
        }

        protected IRestClient Client { get; }

        protected IObjectSerializer Serializer { get; }

        protected virtual async Task<bool> Delete(string path)
        {
            var req = new RestRequest(path, Method.DELETE);
            var response = await this.Client.ExecuteTaskAsync(req);

            return response.IsSuccessful;
        }

        protected virtual async Task<IEnumerable<TResponse>> GetCollection<TResponse>(string path)
        {
            var req = new RestRequest(path, Method.GET);
            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<IEnumerable<TResponse>>(response.Content);
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<IResourcePage<TResponse>> GetCollectionPage<TResponse>(string path)
        {
            var req = new RestRequest(path, Method.GET);
            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<IResourcePage<TResponse>>(response.Content);
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<TResponse> GetSingle<TResponse>(string path)
        {
            var req = new RestRequest(path, Method.GET);
            var response = await this.Client.ExecuteTaskAsync(req);

            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<TResponse>(response.Content);
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<bool> Post<TCreateRequest>(string path, TCreateRequest request)
        {
            var req = new RestRequest(path, Method.POST);
            req.AddJsonBody(request);

            var response = await this.Client.ExecuteTaskAsync(req);
            return response.IsSuccessful;
        }

        protected virtual async Task<TResponse> PostResponse<TCreateRequest, TResponse>(string path, TCreateRequest request)
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

        protected virtual async Task<bool> Put<TUpdateRequest>(string path, TUpdateRequest request)
        {
            var req = new RestRequest(path, Method.PUT);
            req.AddJsonBody(request);

            var response = await this.Client.ExecuteTaskAsync(req);
            return response.IsSuccessful;
        }

        protected virtual async Task<TResponse> PutResponse<TUpdateRequest, TResponse>(string path, TUpdateRequest request)
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
