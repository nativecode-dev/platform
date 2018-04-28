namespace NativeCode.Tests.Clients.Radarr
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Radarr.Extensions;
    using NativeCode.Clients.Radarr.Requests;
    using NativeCode.Core;

    using Xunit;

    public class WhenUsingSonarr : WhenTestingDependencies
    {
        private const string ServerAddress = "http://radarr.in.nativecode.com";

        private static readonly string ApiKey = Environment.GetEnvironmentVariable("APIKEY_RADARR");

        public WhenUsingSonarr()
        {
            var serializer = new JsonObjectSerializer();
            this.Client = new RadarrClient(serializer, new Uri(ServerAddress)).SetApiKey(ApiKey);
        }

        protected RadarrClient Client { get; }

        [Fact]
        public void ShouldCreateClientWithCorrectUrl()
        {
            // Arrange
            // Act
            var sut = new RadarrClient(null, new Uri(ServerAddress));

            // Assert
            Assert.Equal("http://radarr.in.nativecode.com/api/", sut.BaseAddress.ToString());
        }

        [Fact]
        public async Task ShouldGetDiskSpace()
        {
            // Arrange
            // Act
            var disks = await this.Client.DiskSpace.Get();

            // Assert
            Assert.NotEmpty(disks);
        }

        [Fact]
        public async Task ShouldGetMovieCollection()
        {
            // Arrange
            // Act
            var movies = await this.Client.Movies.All();

            // Assert
            Assert.NotEmpty(movies);
        }

        [Fact]
        public async Task ShouldGetSystemStatus()
        {
            // Arrange
            // Act
            var status = await this.Client.System.Get();

            // Assert
            Assert.True(status.IsMonoRuntime);
        }

        [Fact]
        public async Task ShouldGetUpcomingMovies()
        {
            // Arrange
            var query = new QueryCalendar
            {
                Start = DateTimeOffset.Parse("04/20/2018")
            };

            // Act
            var movies = await this.Client.Calendar.Find(query);

            // Assert
            Assert.Equal(7, movies.Count());
        }

        [Fact]
        public async Task ShouldGetCommands()
        {
            // Arrange
            // Act
            var commands = await this.Client.Commands.All();

            // Assert
            Assert.NotNull(commands);
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
