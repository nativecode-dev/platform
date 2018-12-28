namespace NativeCode.Node.Media.Data.Catalog.Images
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using NativeCode.Core.Data;

    public class Image : Entity<Guid>
    {
        public int DisplayHeight { get; set; }

        public int DisplayWidth { get; set; }

        public ImageType ImageType { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public int SourceHeight { get; set; }

        public int SourceWidth { get; set; }

        [MaxLength(4096)]
        public string Text { get; set; }
    }
}
