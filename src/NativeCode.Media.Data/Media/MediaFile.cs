namespace NativeCode.Media.Data.Media
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class MediaFile : Entity<Guid>
    {
        public MediaFileLocationType LocationType { get; set; }

        [MaxLength(DataTypeSize.Text.Short)]
        public string Path { get; set; }
    }
}
