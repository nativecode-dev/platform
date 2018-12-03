namespace NativeCode.Core
{
    using System;
    using Extensions;

    public class FluentUriBuilder : UriBuilder
    {
        public FluentUriBuilder AddQueryParam(string name, string value)
        {
            UriExtensions.AddQueryParam(this, name, value);
            return this;
        }

        public FluentUriBuilder SetHost(string host)
        {
            this.Host = host;
            return this;
        }

        public FluentUriBuilder SetPath(string path)
        {
            this.Path = path;
            return this;
        }

        public FluentUriBuilder SetPort(int port)
        {
            this.Port = port;
            return this;
        }
    }
}