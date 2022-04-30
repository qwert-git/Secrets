using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Secrets.App.Models;
using Secrets.App.Services.SecretsToRawDataConverter;
using Xunit;

namespace Secrets.App.Tests.Services.SecretsToRawDataConverter
{
    public partial class SecretsToJsonConverterTests
    {
        [Fact]
        public void EmptyCollection_Should_Return_EmptyString()
        {
            // Arrange
            var secrets = Enumerable.Empty<Secret>();
            var converter = new SecretsToJsonConverter();

            // Act
            var rawData = converter.GetRaw(secrets);


            // Assert
            rawData.Should().BeEmpty();
        }

        [Fact]
        public void NullCollection_Should_Return_EmptyString()
        {
            // Arrange
            var secrets = null as IEnumerable<Secret>;
            var converter = new SecretsToJsonConverter();

            // Act
            var rawData = converter.GetRaw(secrets);


            // Assert
            rawData.Should().BeEmpty();
        }

        [Fact]
        public void SecretsCollectionNotEmpty_Should_Return_NotEmptyString()
        {
            // Arrange
            var secrets = new Fixture().CreateMany<Secret>();
            var converter = new SecretsToJsonConverter();

            // Act
            var rawData = converter.GetRaw(secrets);


            // Assert
            rawData.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(SingleSecretWithExpectedString))]
        internal void SingleSecret_Json_Should_Equels_Expected(Secret[] secrets, string expectedJson)
        {
            // Arrange
            var converter = new SecretsToJsonConverter();

            // Act
            var rawData = converter.GetRaw(secrets);


            // Assert
            rawData.Should().Be(expectedJson);
        }

        [Theory]
        [MemberData(nameof(ManySecretsWithExpectedString))]
        internal void ManySecrets_Json_Should_Equels_Expected(Secret[] secrets, string expectedJson)
        {
            // Arrange
            var converter = new SecretsToJsonConverter();

            // Act
            var rawData = converter.GetRaw(secrets);


            // Assert
            rawData.Should().Be(expectedJson);
        }
    }
}