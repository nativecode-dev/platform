namespace NativeCode.Media.Data.Media
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class MediaLink : Entity<Guid>
    {
        [MaxLength(DataTypeSize.Identifier.Normal)]
        public string Identifier { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(DataTypeSize.Url)]
        public string Link { get; set; }

        public MediaLinkSource Source { get; set; }
    }
}
