namespace NativeCode.Node.Media.Data.Data.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Images;
    using NativeCode.Core.Data;

    public abstract class MediaInfo : Entity<Guid>
    {
        public string Description { get; set; }

        public List<Image> Images { get; } = new List<Image>();

        public List<MediaProperty> Properties { get; } = new List<MediaProperty>();

        public string SortTitle { get; set; }

        public List<Tag> Tags { get; } = new List<Tag>();

        [Required]
        public string Title { get; set; }
    }
}
