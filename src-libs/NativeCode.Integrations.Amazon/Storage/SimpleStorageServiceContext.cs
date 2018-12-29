namespace NativeCode.Integrations.Amazon.Storage
{
    using Core.Storage;

    public class SimpleStorageServiceContext : RemoteFileStoreContext
    {
        public SimpleStorageServiceContext()
        {
        }

        public SimpleStorageServiceContext(RemoteFileStoreContext clone)
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
