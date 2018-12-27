namespace NativeCode.Node.Media.Data.Metadata
{
    using System;
    using NativeCode.Core.Data;

    public class MetadataSource : Entity<Guid>
    {
        public string Agent { get; set; }

        public string CacheUrl { get; set; }

        public string Provider { get; set; }

        public string SourceUrl { get; set; }
    }
}
