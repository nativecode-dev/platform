namespace NativeCode.Node.Media.Data.Catalog
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using NativeCode.Core.Data;

    public class MetadataSource : Entity<Guid>
    {
        [Required]
        public string Agent { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1056")]
        public string CacheUrl { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        [SuppressMessage("Microsoft.Design", "CA1056")]
        public string SourceUrl { get; set; }
    }
}
