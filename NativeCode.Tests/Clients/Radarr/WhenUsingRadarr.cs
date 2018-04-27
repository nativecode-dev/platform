namespace NativeCode.Tests.Clients.Radarr
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Radarr.Extensions;

    using Xunit;

    public class WhenUsingRadarr : WhenTestingDependencies
    {
        [Fact]
        public void ShouldCreateClientWithCorrectUrl()
        {
            // Arrange
            // Act
            var sut = new RadarrClient(null, new Uri("http://localhost:7878"));

            // Assert
            Assert.Equal("/api/", sut.BaseAddress.PathAndQuery);
        }

        protected override IServiceCollection RegisterServices(IServiceCollection services)
        {
            return services.AddRadarrClient();
        }

        protected override void ReleaseManaged()
        {
        }
    }
}
