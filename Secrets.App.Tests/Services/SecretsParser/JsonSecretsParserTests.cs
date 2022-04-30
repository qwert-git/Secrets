using FluentAssertions;
using Secrets.App.Exceptions;
using Secrets.App.Models;
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
        public void ValidSecretsJson_Should_ReturnNotEmptyCollectionOfSecrets(string rawData)
        {
            // Arrange
            var parser = new JsonSecretsParser();

            // Act
            var secrets = parser.GetSecrets(rawData);
            
            // Assert
            secrets.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(ValidSecretsJsonWithExpectedTestData))]
        internal void ValidSecretsJson_ResultCollection_Should_Equels_ExpectedResult(string rawData, Secret[] expected)
        {
            // Arrange
            var parser = new JsonSecretsParser();

            // Act
            var secrets = parser.GetSecrets(rawData);
            
            // Assert
            secrets.Should().BeEquivalentTo(expected);
        }
    }
}