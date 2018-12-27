namespace NativeCode.Node.Media.Data
{
    using System.Collections.Generic;
    using Metadata;

    public abstract class MediaInfoMetadata : MediaInfo
    {
        public List<MetadataSource> MetadataSources { get; set; }
    }
}
