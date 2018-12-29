namespace NativeCode.Node.Media.Data.Data.Catalog
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using NativeCode.Core.Data;

    public class MetadataSource : Entity<Guid>
    {
        [Required]
        public string Agent { get; set; }

        public string CacheUrl { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string SourceUrl { get; set; }
    }
}
