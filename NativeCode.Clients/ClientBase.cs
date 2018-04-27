namespace NativeCode.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Cache;
    using System.Text;

    using JetBrains.Annotations;

    using NativeCode.Core;

    using RestSharp;
    using RestSharp.Authenticators;

    public abstract class ClientBase : IClient
    {
        private readonly Dictionary<string, IResource> resources = new Dictionary<string, IResource>();

        protected ClientBase(IObjectSerializer serializer, string baseAddress)
            : this(serializer, new Uri(baseAddress))
        {
        }

        protected ClientBase(IObjectSerializer serializer, Uri baseAddress)
        {
            this.BaseAddress = baseAddress;

            this.Client = new RestClient(baseAddress)
            {
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable),
                CookieContainer = new CookieContainer(),
                Encoding = Encoding.UTF8,
                FollowRedirects = true
            };

            this.Serializer = serializer;
        }

        public Uri BaseAddress { get; }

        protected IRestClient Client { get; }

        protected IObjectSerializer Serializer { get; }

        public void SetBasicAuth(string username, string password)
        {
            this.Client.Authenticator = new HttpBasicAuthenticator(username, password);
        }

        public void SetCookieContainer([CanBeNull] CookieContainer container)
        {
            this.Client.CookieContainer = container;
        }

        protected void AddResource<TRequest, TResponse>(IResource<TRequest, TResponse> resource)
        {
            var key = $"{typeof(TRequest).Name}:{typeof(TResponse).Name}";

            if (this.resources.ContainsKey(key) == false)
            {
                this.resources.Add(key, resource);
            }
        }

        protected IResource<TRequest, TResponse> GetResource<TRequest, TResponse>()
        {
            var key = $"{typeof(TRequest).Name}:{typeof(TResponse).Name}";

            if (this.resources.ContainsKey(key))
            {
                return (IResource<TRequest, TResponse>)this.resources[key];
            }

            throw new KeyNotFoundException(key);
        }
    }
}
