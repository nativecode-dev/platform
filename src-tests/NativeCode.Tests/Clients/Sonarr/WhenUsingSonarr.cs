namespace NativeCode.Tests.Clients.Sonarr
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using NativeCode.Clients.Radarr.Extensions;
    using NativeCode.Clients.Sonarr;
    using Xunit;

    public class WhenUsingSonarr : WhenTestingDependencies
    {
        private const string ServerAddress = "http://sonarr.in.nativecode.com";

        private static readonly string ApiKey = Environment.GetEnvironmentVariable("APIKEY_SONARR");

        protected override IServiceCollection RegisterServices(IServiceCollection services)
        {
            return services.AddRadarrClient();
        }

        protected override void ReleaseManaged()
        {
        }

        [Fact]
        public void ShouldCreateClientWithCorrectUrl()
        {
            // Arrange
            // Act
            var sut = new SonarrClient(null, new Uri(ServerAddress));

            // Assert
            Assert.Equal("http://sonarr.in.nativecode.com/api/", sut.BaseAddress.ToString());
        }
    }
}