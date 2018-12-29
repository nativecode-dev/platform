namespace NativeCode.Core.Credentials
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.Runtime;
    using Nito.AsyncEx;

    public class AwsEnvironmentCredentialProvider : IAwsCredentialProvider
    {
        private const string AwsAccessKey = "AWS_ACCESS_KEY";

        private const string AwsRegion = "AWS_REGION";

        private const string AwsSecretKey = "AWS_SECRET_KEY";

        public AwsEnvironmentCredentialProvider()
        {
            var env = Environment.GetEnvironmentVariable(AwsRegion);
            var region = string.IsNullOrWhiteSpace(env) ? "us-east-1" : env;
            this.Region = RegionEndpoint.GetBySystemName(region);
        }

        public RegionEndpoint Region { get; }

        public AWSCredentials GetCredentials()
        {
            return AsyncContext.Run(() => this.GetCredentialsAsync(CancellationToken.None));
        }

        public Task<AWSCredentials> GetCredentialsAsync()
        {
            return this.GetCredentialsAsync(CancellationToken.None);
        }

        public Task<AWSCredentials> GetCredentialsAsync(CancellationToken cancellationToken)
        {
            var accessKey = Environment.GetEnvironmentVariable(AwsAccessKey);
            var secretKey = Environment.GetEnvironmentVariable(AwsSecretKey);

            if (string.IsNullOrWhiteSpace(accessKey) || string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException($"Could not environment variable for either {AwsAccessKey} or {AwsSecretKey}.");
            }

            AWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);

            return Task.FromResult(credentials);
        }
    }
}
