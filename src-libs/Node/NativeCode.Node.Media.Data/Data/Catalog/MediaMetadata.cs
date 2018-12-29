namespace NativeCode.Node.Media.Data.Data.Catalog
{
    using System.Collections.Generic;

    public abstract class MediaMetadata : MediaInfo
    {
        public List<MetadataSource> MetadataSources { get; } = new List<MetadataSource>();
    }
}
