namespace NativeCode.Node.Media.Data.Catalog
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using NativeCode.Core.Data;

    public class MediaProperty : Entity<Guid>
    {
        [Required]
        [MaxLength(256)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [MaxLength(1024)]
        public string ValueType { get; set; }
    }
}
