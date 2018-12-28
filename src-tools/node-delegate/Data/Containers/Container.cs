namespace node_delegate.Data.Containers
{
    using System;
    using System.Collections.Generic;

    using Docker.DotNet.Models;

    using NativeCode.Core.Data;

    public class Container : Entity<Guid>
    {
        public ContainerConfiguration Configuration { get; set; }
    }

    public class ContainerConfiguration : Entity<Guid>
    {
        public bool ArgsEscaped { get; set; }

        public bool AttachStdErr { get; set; }

        public bool AttachStdIn { get; set; }

        public bool AttachStdOut { get; set; }

        public IList<string> Cmd { get; set; } = new List<string>();

        public string DomainName { get; set; }

        public IList<string> Endpoint { get; set; } = new List<string>();

        public IList<string> Env { get; set; } = new List<string>();

        public string HostName { get; set; }

        public string Image { get; set; }

        public IDictionary<string, string> Labels { get; set; } = new Dictionary<string, string>();

        public string MacAddress { get; set; }

        public string Name { get; set; }

        public bool NetworkDisabled { get; set; }

        public IList<string> OnBuild { get; set; } = new List<string>();

        public bool OpenStdIn { get; set; }

        public IDictionary<int, int> Ports { get; set; } = new Dictionary<int, int>();

        public IList<string> Shell { get; set; }

        public bool StdInOnce { get; set; }

        public string StopSignal { get; set; }

        public TimeSpan? StopTimeout { get; set; }

        public bool Tty { get; set; }

        public string User { get; set; }

        public string WorkDir { get; set; }
    }

    public class EndpointSettings
    {
        public IList<string> Aliases { get; set; }

        public string EndpointID { get; set; }

        public string Gateway { get; set; }

        public string GlobalIPv6Address { get; set; }

        public long GlobalIPv6PrefixLen { get; set; }

        public string IPAddress { get; set; }

        public EndpointIPAMConfig IPAMConfig { get; set; }

        public long IPPrefixLen { get; set; }

        public string IPv6Gateway { get; set; }

        public IList<string> Links { get; set; }

        public string MacAddress { get; set; }

        public string NetworkID { get; set; }
    }
}
