namespace NativeCode.Clients
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using RestSharp;

    public abstract class ResourceBase : IResource
    {
        private readonly ConcurrentDictionary<string, string> cache = new ConcurrentDictionary<string, string>();

        protected ResourceBase(IRestClient client, IObjectSerializer serializer)
        {
            this.Client = client;
            this.Serializer = serializer;
        }

        protected IRestClient Client { get; }

        protected IObjectSerializer Serializer { get; }

        protected virtual IRestRequest CreateRequest(string path, Method method)
        {
            return new RestRequest(path, method)
            {
                JsonSerializer = new RestSerializer(this.Serializer)
            };
        }

        protected virtual IRestRequest CreateRequest<T>(string path, Method method, T body)
        {
            return this.CreateRequest(path, method)
                .AddJsonBody(body);
        }

        protected virtual T CreateResponse<T>(IRestResponse response)
        {
            if (response.IsSuccessful)
            {
                return this.Serializer.Deserialize<T>(response.Content);
            }

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            throw new InvalidOperationException(response.ErrorMessage);
        }

        protected virtual Task<bool> Delete(string path)
        {
            var request = this.CreateRequest(path, Method.DELETE);
            return this.Execute(request);
        }

        protected virtual async Task<bool> Execute(IRestRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var key = $"{request.Method}.{request.Resource.Replace("/", "-")}".GetGuid()
                .ToString();

            if (this.cache.ContainsKey(key))
            {
                return this.Serializer.Deserialize<bool>(this.cache[key]);
            }

            var response = await this.Client.ExecuteTaskAsync(request, cancellationToken);
            var content = this.Serializer.Serialize(response.IsSuccessful);

            this.cache.AddOrUpdate(key, k => content, (k, v) => content);

            return response.IsSuccessful;
        }

        protected virtual Task<IEnumerable<TResponse>> GetCollection<TResponse>(string path)
        {
            var request = this.CreateRequest(path, Method.GET);
            return this.Returns<IEnumerable<TResponse>>(request);
        }

        protected virtual Task<IResourcePage<TResponse>> GetCollectionPage<TResponse>(string path)
        {
            var request = this.CreateRequest(path, Method.GET);
            return this.Returns<IResourcePage<TResponse>>(request);
        }

        protected virtual Task<TResponse> GetSingle<TResponse>(string path)
        {
            var request = this.CreateRequest(path, Method.GET);
            return this.Returns<TResponse>(request);
        }

        protected virtual Task<bool> Post<TRequest>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.POST, value);
            return this.Execute(request);
        }

        protected virtual Task<TResponse> PostResponse<TRequest, TResponse>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.POST, value);
            return this.Returns<TResponse>(request);
        }

        protected virtual Task<bool> Put<TRequest>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.PUT, value);
            return this.Execute(request);
        }

        protected virtual Task<TResponse> PutResponse<TRequest, TResponse>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.PUT, value);
            return this.Returns<TResponse>(request);
        }

        protected virtual async Task<T> Returns<T>(IRestRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var key = $"{request.Method}.{request.Resource.Replace("/", "-")}".GetGuid()
                .ToString();

            if (this.cache.ContainsKey(key))
            {
                return this.Serializer.Deserialize<T>(this.cache[key]);
            }

            var response = await this.Client.ExecuteTaskAsync(request, cancellationToken);

            this.cache.AddOrUpdate(key, k => response.Content, (k, v) => response.Content);

            return this.CreateResponse<T>(response);
        }
    }
}