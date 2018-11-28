namespace NativeCode.Clients.Posteio
{
    using System;
    using System.Threading.Tasks;
    using Core.Serialization;
    using Responses;
    using RestSharp;

    public abstract class ClientResource<TResource, TResourceCreate, TResourceUpdate>
    {
        private readonly RestClient client;

        protected internal ClientResource(RestClient client)
        {
            this.client = client;
        }

        protected virtual async Task<bool> CreateResource(string path, TResourceCreate resource)
        {
            var request = new RestRequest(path, Method.POST)
            {
                JsonSerializer = new RestSerializer(new JsonObjectSerializer()),
                RequestFormat = DataFormat.Json,
            };
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(resource);

            var response = await this.client.ExecuteTaskAsync(request);
            return response.IsSuccessful;
        }

        protected virtual async Task<bool> DeleteResource(string path)
        {
            var request = new RestRequest(path, Method.DELETE)
            {
                JsonSerializer = new RestSerializer(new JsonObjectSerializer()),
                RequestFormat = DataFormat.Json,
            };
            request.AddHeader("Content-Type", "application/json");

            var response = await this.client.ExecuteTaskAsync(request);

            return response.IsSuccessful;
        }

        protected virtual async Task<TResource> GetResource(string path)
        {
            var request = new RestRequest(path, Method.GET)
            {
                JsonSerializer = new RestSerializer(new JsonObjectSerializer()),
                RequestFormat = DataFormat.Json,
            };
            request.AddHeader("Content-Type", "application/json");

            var response = await this.client.ExecuteTaskAsync<TResource>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<bool> PutResource(string path, TResource resource)
        {
            var request = new RestRequest(path, Method.PUT)
            {
                JsonSerializer = new RestSerializer(new JsonObjectSerializer()),
                RequestFormat = DataFormat.Json,
            };
            request.AddHeader("Content-Type", "application/json");

            var response = await this.client.ExecuteTaskAsync(request);

            return response.IsSuccessful;
        }

        protected virtual async Task<ResponsePage<TResource>> QueryResource(string path)
        {
            var request = new RestRequest(path, Method.GET)
            {
                JsonSerializer = new RestSerializer(new JsonObjectSerializer()),
                RequestFormat = DataFormat.Json,
            };
            request.AddHeader("Content-Type", "application/json");

            var response = await this.client.ExecuteTaskAsync<ResponsePage<TResource>>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<bool> UpdateResource(string path, TResourceUpdate resource)
        {
            var request = new RestRequest(path, Method.PATCH)
            {
                JsonSerializer = new RestSerializer(new JsonObjectSerializer()),
                RequestFormat = DataFormat.Json,
            };
            request.AddHeader("Content-Type", "application/json");

            var response = await this.client.ExecuteTaskAsync(request);

            return response.IsSuccessful;
        }

        protected virtual async Task<TResource> UpdateResourceReturns(string path, TResourceUpdate resource)
        {
            var request = new RestRequest(path, Method.PATCH)
            {
                JsonSerializer = new RestSerializer(new JsonObjectSerializer()),
                RequestFormat = DataFormat.Json,
            };
            request.AddHeader("Content-Type", "application/json");

            var response = await this.client.ExecuteTaskAsync<TResource>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException();
        }
    }
}