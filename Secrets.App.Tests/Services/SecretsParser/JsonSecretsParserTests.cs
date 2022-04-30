using FluentAssertions;
using Secrets.App.Exceptions;
using Secrets.App.Services.SecretsParser;
using Xunit;

namespace Secrets.App.Tests.Services.SecretsParser
{
    public partial class JsonSecretsParserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void EmptyOrNullString_Should_ReturnEmptyCollection(string rawData)
        {
            // Arrange
            var parser = new JsonSecretsParser();

            // Act
            var secrets = parser.GetSecrets(rawData);

            // Assert
            secrets.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(InvalidFormatTestData))]
        public void InvalidJsonFormat_Should_ThrowException(string rawData)
        {
            // Arrange
            var parser = new JsonSecretsParser();

            // Assert
            Assert.Throws<SecretsAppException>(() => parser.GetSecrets(rawData));
        }

        [Theory]
        [MemberData(nameof(InvalidJsonSchemeTestData))]
        public void InvalidJsonScheme_Should_ThrowException(string rawData)
        {
            // Arrange
            var parser = new JsonSecretsParser();

            // Assert
            Assert.Throws<SecretsAppException>(() => parser.GetSecrets(rawData));
        }

        [Theory]
        [MemberData(nameof(ValidSecretsJsonTestData))]
        public void ValidSecretsJson_Should_ReturnCollectionOfSecrets(string rawData)
        {
            // Arrange
            var parser = new JsonSecretsParser();

            // Act
            var secrets = parser.GetSecrets(rawData);
            
            // Assert
            secrets.Should().NotBeEmpty();
        }
    }
}