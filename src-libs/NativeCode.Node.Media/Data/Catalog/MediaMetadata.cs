namespace NativeCode.Node.Media.Data.Catalog
{
    using System.Collections.Generic;

    public abstract class MediaMetadata : MediaInfo
    {
        public List<MetadataSource> MetadataSources { get; } = new List<MetadataSource>();
    }
}
