using AutoFixture;
using FluentAssertions;
using Secrets.App.Models;
using Secrets.Services.SecretsConverter;
using Xunit;

namespace Secrets.App.Tests.SecretsToRawDataConverter.Services;

public class SecretsToPlainTextDataConverterTests
{
    [Fact]
    public void ConvertOneSecrets_Should_Return_SingleRowString()
    {
        // Arrange
        var secretToValidate = new Fixture().Create<Secret>();
        var converter = new SecretsToPlainTextDataConverter();

        // Act
        var result = converter.GetRaw(new [] { secretToValidate });

        // Assert
        result.Should().Be($"{secretToValidate.Key} {secretToValidate.Login} {secretToValidate.Password}");
    }

    [Fact]
    public void ConvertTwoSecrets_Should_Return_TwoRowString()
    {
        // Arrange
        var fixture = new Fixture();
        var secretToValidate1 = fixture.Create<Secret>();
        var secretToValidate2 = fixture.Create<Secret>();

        var converter = new SecretsToPlainTextDataConverter();

        var secrets = new [] { secretToValidate1, secretToValidate2 };

        // Act
        var result = converter.GetRaw(secrets);

        // Assert
        result.Should().Be(
            $"{secretToValidate1.Key} {secretToValidate1.Login} {secretToValidate1.Password}" +
            $"\r\n{secretToValidate2.Key} {secretToValidate2.Login} {secretToValidate2.Password}"
            );
    }
}