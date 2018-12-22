namespace NativeCode.Core.Aws
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
        public AwsOptionsCredentialProvider(IOptions<AWS> options)
        {
            this.Options = options.Value;
            this.Region = RegionEndpoint.GetBySystemName(this.Options.Region);
        }

        protected AWS Options { get; }

        public RegionEndpoint Region { get; set; }

        public AWSCredentials GetCredentials()
        {
            return AsyncContext.Run(() => this.GetCredentialsAsync(CancellationToken.None));
        }

        public Task<AWSCredentials> GetCredentialsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AWSCredentials credentials = new BasicAWSCredentials(this.Options.AccessKey, this.Options.Secretkey);

            return Task.FromResult(credentials);
        }
    }
}
