namespace NativeCode.Core.Credentials
{
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.Runtime;
    using Microsoft.Extensions.Options;
    using Nito.AsyncEx;
    using Options;

    public class AwsOptionsCredentialProvider : IAwsCredentialProvider
    {
        public AwsOptionsCredentialProvider(IOptions<AwsOptions> options)
        {
            this.Options = options.Value;
            this.Region = RegionEndpoint.GetBySystemName(this.Options.Region);
        }

        public RegionEndpoint Region { get; set; }

        protected AwsOptions Options { get; }

        public AWSCredentials GetCredentials()
        {
            return AsyncContext.Run(() => this.GetCredentialsAsync(CancellationToken.None));
        }

        /// <inheritdoc />
        public Task<AWSCredentials> GetCredentialsAsync()
        {
            return this.GetCredentialsAsync(CancellationToken.None);
        }

        public Task<AWSCredentials> GetCredentialsAsync(CancellationToken cancellationToken)
        {
            AWSCredentials credentials = new BasicAWSCredentials(this.Options.AccessKey, this.Options.Secretkey);

            return Task.FromResult(credentials);
        }
    }
}
