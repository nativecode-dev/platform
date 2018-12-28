namespace NativeCode.Node.Media.Data.Data.Catalog
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using NativeCode.Core.Data;

    public class Tag : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }

        public string Normalized => this.Name.ToUpperInvariant();
    }
}
