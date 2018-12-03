namespace NativeCode.Clients
{
    using System;
    using System.Net;
    using System.Net.Cache;
    using System.Text;
    using Core;
    using Core.Serialization;
    using RestSharp;
    using RestSharp.Authenticators;

    public abstract class CommonClient : ICommonClient
    {
        protected CommonClient(IObjectSerializer serializer, string address)
            : this(serializer, new Uri(address))
        {
        }

        protected CommonClient(IObjectSerializer serializer, Uri address)
        {
            this.BaseAddress = address;

            this.Client = new RestClient(this.BaseAddress)
            {
                AutomaticDecompression = true,
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable),
                CookieContainer = new CookieContainer(),
                Encoding = Encoding.UTF8,
                FollowRedirects = true,
                UserAgent = GetUserAgentString(this.GetType())
            };

            this.Serializer = serializer;
        }

        protected IRestClient Client { get; }

        protected IObjectSerializer Serializer { get; }

        public Uri BaseAddress { get; }

        public void SetBasicAuth(string username, string password)
        {
            this.Client.Authenticator = new HttpBasicAuthenticator(username, password);
        }

        public void SetCookieContainer(CookieContainer container)
        {
            this.Client.CookieContainer = container;
        }

        private static string GetUserAgentString(Type type)
        {
            var agent = $"common-client/{Info.AppVersion(type)}";
            var platform =
                $"{Environment.OSVersion.Platform}; {Environment.OSVersion.VersionString}; {Environment.OSVersion.ServicePack}";
            var runtime = $"common-client-runtime/{Environment.Version}";

            return $"{agent} ({platform}) {runtime}";
        }
    }
}