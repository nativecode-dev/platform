namespace NativeCode.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Core;

    using RestSharp;
    using RestSharp.Serializers;

    public abstract class ResourceBase : IResource
    {
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
            return this.CreateRequest(path, method).AddJsonBody(body);
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

        protected virtual async Task<bool> Delete(string path)
        {
            var request = this.CreateRequest(path, Method.DELETE);
            var response = await this.Client.ExecuteTaskAsync(request);

            return response.IsSuccessful;
        }

        protected virtual async Task<IEnumerable<TResponse>> GetCollection<TResponse>(string path)
        {
            var request = this.CreateRequest(path, Method.GET);
            var response = await this.Client.ExecuteGetTaskAsync(request);

            return this.CreateResponse<IEnumerable<TResponse>>(response);
        }

        protected virtual async Task<IResourcePage<TResponse>> GetCollectionPage<TResponse>(string path)
        {
            var request = this.CreateRequest(path, Method.GET);
            var response = await this.Client.ExecuteGetTaskAsync(request);

            return this.CreateResponse<IResourcePage<TResponse>>(response);
        }

        protected virtual async Task<TResponse> GetSingle<TResponse>(string path)
        {
            var request = this.CreateRequest(path, Method.GET);
            var response = await this.Client.ExecuteGetTaskAsync(request);

            return this.CreateResponse<TResponse>(response);
        }

        protected virtual async Task<bool> Post<TRequest>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.POST, value);
            var response = await this.Client.ExecutePostTaskAsync(request);

            return response.IsSuccessful;
        }

        protected virtual async Task<TResponse> PostResponse<TRequest, TResponse>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.POST, value);
            var response = await this.Client.ExecutePostTaskAsync(request);

            return this.CreateResponse<TResponse>(response);
        }

        protected virtual async Task<bool> Put<TRequest>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.PUT, value);
            var response = await this.Client.ExecuteTaskAsync(request);

            return response.IsSuccessful;
        }

        protected virtual async Task<TResponse> PutResponse<TRequest, TResponse>(string path, TRequest value)
        {
            var request = this.CreateRequest(path, Method.PUT, value);
            var response = await this.Client.ExecuteTaskAsync(request);

            return this.CreateResponse<TResponse>(response);
        }

        public class RestSerializer : ISerializer
        {
            private readonly IObjectSerializer serializer;

            public RestSerializer(IObjectSerializer serializer)
            {
                this.serializer = serializer;
            }

            public string ContentType { get; set; } = "application/json";

            public string DateFormat { get; set; }

            public string Namespace { get; set; }

            public string RootElement { get; set; }

            public string Serialize(object obj)
            {
                return this.serializer.Serialize(obj);
            }
        }
    }
}
