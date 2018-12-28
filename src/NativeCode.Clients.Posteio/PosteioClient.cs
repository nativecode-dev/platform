namespace NativeCode.Clients.Posteio
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Cache;
    using System.Text;
    using Extensions;
    using Resources;
    using RestSharp;
    using RestSharp.Authenticators;

    public class PosteioClient : IPosteioClient
    {
        private const string PosteioAgent = "posteio-user-agent";

        private const string PosteioUrlFormat = "admin/api/{0}/";

        public PosteioClient(string hostname, string username, string password,
            ClientVersion version = ClientVersion.Default)
            : this(GetPosteioAdminApi(hostname, version), username, password)
        {
        }

        public PosteioClient(Uri baseAddress, string username, string password)
        {
            var client = new RestClient(baseAddress)
            {
                Authenticator = new HttpBasicAuthenticator(username, password),
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable),
                CookieContainer = new CookieContainer(),
                Encoding = Encoding.UTF8,
                FollowRedirects = true,
                UserAgent = PosteioAgent,
            };

            this.Domains = new DomainResource(client);
            this.Mailboxes = new MailboxResource(client);
        }

        public DomainResource Domains { get; }

        public MailboxResource Mailboxes { get; }

        protected internal static Uri GetPosteioAdminApi(string hostname, ClientVersion version)
        {
            var builder = new UriBuilder(Uri.UriSchemeHttps, hostname)
            {
                Path = string.Format(CultureInfo.CurrentCulture, PosteioUrlFormat, version.AsPathString())
            };

            return builder.Uri;
        }
    }
}
