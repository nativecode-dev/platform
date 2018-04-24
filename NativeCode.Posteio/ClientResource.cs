namespace NativeCode.Posteio
{
    using System;
    using System.Threading.Tasks;

    using NativeCode.Posteio.Responses;

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
            var request = new RestRequest(path, Method.POST);
            request.AddBody(resource);

            var response = await this.client.ExecuteTaskAsync(request);
            return response.IsSuccessful;
        }

        protected virtual async Task<bool> DeleteResource(string path)
        {
            var request = new RestRequest(path, Method.DELETE);
            var response = await this.client.ExecuteTaskAsync(request);
            return response.IsSuccessful;
        }

        protected virtual async Task<TResource> GetResource(string path)
        {
            var request = new RestRequest(path, Method.GET);
            var response = await this.client.ExecuteTaskAsync<TResource>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<bool> PutResource(string path, TResource resource)
        {
            var request = new RestRequest(path, Method.PUT);
            var response = await this.client.ExecuteTaskAsync(request);
            return response.IsSuccessful;
        }

        protected virtual async Task<ResponsePage<TResource>> QueryResource(string path)
        {
            var request = new RestRequest(path, Method.GET);
            var response = await this.client.ExecuteTaskAsync<ResponsePage<TResource>>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException();
        }

        protected virtual async Task<bool> UpdateResource(string path, TResourceUpdate resource)
        {
            var request = new RestRequest(path, Method.PATCH);
            var response = await this.client.ExecuteTaskAsync(request);
            return response.IsSuccessful;
        }

        protected virtual async Task<TResource> UpdateResourceReturns(string path, TResourceUpdate resource)
        {
            var request = new RestRequest(path, Method.PATCH);
            var response = await this.client.ExecuteTaskAsync<TResource>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException();
        }
    }
}
