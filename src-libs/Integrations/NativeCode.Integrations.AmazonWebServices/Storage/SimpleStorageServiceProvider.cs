namespace NativeCode.Integrations.AmazonWebServices.Storage
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon.S3;
    using Amazon.S3.Model;
    using Core.Credentials;
    using Core.Extensions;
    using Core.Reliability;
    using Core.Storage;

    public class SimpleStorageServiceProvider : Disposable, IRemoteFileStoreProvider
    {
        public SimpleStorageServiceProvider(IAwsCredentialProvider awsCredentialProvider)
        {
            var credentials = awsCredentialProvider.GetCredentials();
            this.Client = new AmazonS3Client(credentials);
        }

        /// <inheritdoc />
        public string Scheme => "s3:";

        protected AmazonS3Client Client { get; }

        /// <inheritdoc />
        public bool CanAdapt(Uri location)
        {
            return location.Scheme == this.Scheme;
        }

        /// <inheritdoc />
        public Task<bool> CanConnect(Uri location)
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public RemoteFileStoreContext CreateContext(Uri location)
        {
            return new SimpleStorageServiceContext(location);
        }

        /// <inheritdoc />
        public async Task<Stream> RetrieveAsync(RemoteFileStoreContext context, CancellationToken cancellationToken)
        {
            var ctx = (SimpleStorageServiceContext)context;
            var request = new GetObjectRequest
            {
                BucketName = ctx.BucketName,
                Key = ctx.FilePath,
            };

            var response = await this.Client.GetObjectAsync(request, cancellationToken)
                .NoCapture();

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return response.ResponseStream;
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public async Task StoreAsync(RemoteFileStoreContext context, Stream stream, CancellationToken cancellationToken)
        {
            var ctx = (SimpleStorageServiceContext)context;

            var request = new PutObjectRequest
            {
                BucketName = ctx.BucketName,
                FilePath = Path.GetDirectoryName(ctx.FilePath),
                Key = ctx.FilePath,
            };

            var response = await this.Client.PutObjectAsync(request, cancellationToken)
                .NoCapture();

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return;
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        protected override void ReleaseManaged()
        {
            this.Client.Dispose();
        }
    }
}
