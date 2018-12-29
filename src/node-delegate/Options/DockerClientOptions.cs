namespace node_delegate.Options
{
    using System;
    using Docker.DotNet;

    public class DockerClientOptions
    {
        public Credentials Credentials { get; set; }

        public TimeSpan Timeout { get; set; }

        public Uri Url { get; set; }

        public Version Version { get; set; }

        public DockerClientConfiguration Configuration()
        {
            return new DockerClientConfiguration(this.Url, this.Credentials, this.Timeout);
        }
    }
}
