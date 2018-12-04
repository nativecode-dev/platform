namespace NativeCode.Tests.Core
{
    using System;
    using NativeCode.Core.Extensions;
    using Xunit;

    public class WhenParsingUriQueryParams : WhenTesting
    {
        private static readonly Uri NoQueryParams = new Uri("http://localhost");

        private static readonly Uri SimpleQueryParams = new Uri("http://localhost/?name1=value1&name2=value2");

        protected override void ReleaseManaged()
        {
        }

        [Fact]
        public void ShouldReturnEmptyDictionary()
        {
            // Arrange
            // Act
            var parameters = NoQueryParams.ParseQueryParams();

            // Assert
            Assert.Empty(parameters.Keys);
        }

        [Fact]
        public void ShouldReturnQueryParamsAsDictionary()
        {
            // Arrange
            // Act
            var parameters = SimpleQueryParams.ParseQueryParams();

            // Assert
            Assert.Equal(2, parameters.Keys.Count);
            Assert.Equal(2, parameters.Values.Count);
        }
    }
}
