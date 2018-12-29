namespace NativeCode.Integrations.AmazonWebServices.Storage
{
    using System;
    using Core.Storage;

    public class SimpleStorageServiceContext : RemoteFileStoreContext
    {
        public SimpleStorageServiceContext(Uri location)
        {
            this.Host = location.DnsSafeHost;
            this.BucketName = location.Segments[0];
        }

        protected SimpleStorageServiceContext(RemoteFileStoreContext clone)
            : base(clone)
        {
        }

        public string BucketName { get; set; }

        /// <inheritdoc />
        public override RemoteFileStoreContext Clone()
        {
            return new SimpleStorageServiceContext(this)
            {
                BucketName = this.BucketName,
            };
        }
    }
}
