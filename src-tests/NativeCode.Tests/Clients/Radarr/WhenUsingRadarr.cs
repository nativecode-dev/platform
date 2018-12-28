namespace NativeCode.Tests.Clients.Radarr
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using NativeCode.Clients.Radarr;
    using NativeCode.Clients.Radarr.Requests;
    using Xunit;

    public class WhenUsingSonarr : WhenTestingRadarr
    {
        [Fact]
        public void ShouldCreateClientWithCorrectUrl()
        {
            // Arrange
            // Act
            var sut = new RadarrClient(null, new Uri(ServerAddress));

            // Assert
            Assert.Equal("http://in.nativecode.com:7878/api/", sut.BaseAddress.ToString());
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

        [Fact]
        public async Task ShouldGetDiskSpace()
        {
            // Arrange
            // Act
            var disks = await this.Client.DiskSpace.GetResource();

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
            var status = await this.Client.System.GetResource();

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
    }
}
