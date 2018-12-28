namespace NativeCode.Core.Aws
{
    using System.Threading;
    using System.Threading.Tasks;

    using Amazon;
    using Amazon.Runtime;

    /// <summary>
    /// Provides AWS credentials.
    /// </summary>
    public interface IAwsCredentialProvider
    {
        /// <summary>
        /// Gets the AWS region to use.
        /// </summary>
        RegionEndpoint Region { get; }

        /// <summary>
        /// Returns AWS credentials.
        /// </summary>
        /// <returns></returns>
        AWSCredentials GetCredentials();

        /// <summary>
        /// Returns AWS credentials.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AWSCredentials> GetCredentialsAsync(CancellationToken cancellationToken = default);
    }
}
