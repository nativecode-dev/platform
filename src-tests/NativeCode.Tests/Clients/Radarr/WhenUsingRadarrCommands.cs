namespace NativeCode.Tests.Clients.Radarr
{
    using System.Linq;
    using System.Threading.Tasks;
    using NativeCode.Clients.Radarr.Requests.Commands;
    using Xunit;

    public class WhenUsingRadarrCommands : WhenTestingRadarr
    {
        [Fact]
        public async Task ShouldExecuteRssSync()
        {
            // Arrange
            var options = new RssSyncOptions();

            // Act
            var command = await this.Client.Commands.Run(options);
            var commands = await this.Client.Commands.All();

            // Assert
            Assert.Contains(command.Id, commands.Select(c => c.Id));
        }
    }
}