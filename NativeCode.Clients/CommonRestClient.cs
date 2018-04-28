namespace NativeCode.Clients
{
    using System;
    using System.Net;
    using System.Net.Cache;
    using System.Text;

    using NativeCode.Core;

    using RestSharp;
    using RestSharp.Authenticators;

    public abstract class CommonRestClient : IClient
    {
        protected CommonRestClient(IObjectSerializer serializer, string baseAddress)
            : this(serializer, new Uri(baseAddress))
        {
        }

        protected CommonRestClient(IObjectSerializer serializer, Uri baseAddress)
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

        public void SetCookieContainer(CookieContainer container)
        {
            this.Client.CookieContainer = container;
        }
    }
}
