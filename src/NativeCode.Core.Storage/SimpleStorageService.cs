namespace NativeCode.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Amazon.S3;
    using Amazon.S3.Transfer;
    using Aws;
    using Microsoft.Extensions.Logging;
    using Reliability;

    public class SimpleStorageService : Disposable, IStorageService
    {
        public SimpleStorageService(IAwsCredentialProvider aws, ILogger<SimpleStorageService> logger)
        {
            this.Aws = aws;
            this.Client = new AmazonS3Client(aws.GetCredentials());
            this.Logger = logger;
        }

        protected IAwsCredentialProvider Aws { get; }

        protected AmazonS3Client Client { get; }

        protected ILogger<SimpleStorageService> Logger { get; }

        public Task<Stream> RetrieveStream(string bucket, string key, Stream stream)
        {
            try
            {
                using (var transfer = new TransferUtility(this.Client))
                {
                    var request = new TransferUtilityOpenStreamRequest
                    {
                        BucketName = bucket,
                        Key = key,
                    };

                    return transfer.OpenStreamAsync(request);
                }
            }
            catch (AmazonS3Exception ex)
            {
                this.Logger.LogError("{@ex}", ex);
                throw;
            }
            catch (Exception ex)
            {
                this.Logger.LogError("{@ex}", ex);
                throw;
            }
        }

        public Task StoreStream(string bucket, string key, Stream stream)
        {
            try
            {
                using (var transfer = new TransferUtility(this.Client))
                {
                    return transfer.UploadAsync(stream, bucket, key);
                }
            }
            catch (AmazonS3Exception ex)
            {
                this.Logger.LogError("{@ex}", ex);
                throw;
            }
            catch (Exception ex)
            {
                this.Logger.LogError("{@ex}", ex);
                throw;
            }
        }

        protected override void ReleaseManaged()
        {
            this.Client.Dispose();
        }
    }
}
