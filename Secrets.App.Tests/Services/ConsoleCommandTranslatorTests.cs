using System;
using FluentAssertions;
using Secrets.App.Services;
using Xunit;

namespace Secrets.App.Tests.Services;
public class ConsoleCommandTranslatorTests
{

    [Fact]
    public void AddCommand_EmptyArgs_Should_ReturnFalse()
    {
        // Arrange
        var args = Array.Empty<string>();
        var translator = new ConsoleCommandTranslator(args);

        // Act
        var result = translator.IsAddNew();

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(new string[] { "" }, false)]
    [InlineData(new string[] { "new" }, false)]
    [InlineData(new string[] { "add" }, true)]
    public void AddCommand_CommandName_Should_ReturnExpectedResult(string[] args, bool expectedResult)
    {
        // Arrange
        var translator = new ConsoleCommandTranslator(args);

        // Act
        var result = translator.IsAddNew();

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(new string[] { "" }, false)]
    [InlineData(new string[] { "new" }, false)]
    [InlineData(new string[] { "add" }, false)]
    [InlineData(new string[] { "add", "key_name" }, false)]
    [InlineData(new string[] { "add", "key_name", "login" }, false)]
    public void GetNewSecret_NoArguments_Should_ReturnNull(string[] args, bool _)
    {
        // Arrange
        var translator = new ConsoleCommandTranslator(args);

        // Act
        var result = translator.GetNewSecret();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNewSecret_ArgumentsExists_Should_ReturnCorrectSecret()
    {
        // Arrange
        const string CommnadName = "add";
        const string TestSecretKey = "secret_key";
        const string TestLogin = "login";
        const string TestPassword = "password";

        var args = new string[] { CommnadName, TestSecretKey, TestLogin, TestPassword };
        var translator = new ConsoleCommandTranslator(args);

        // Act
        var secret = translator.GetNewSecret();

        // Assert
        secret.Key.Should().Be(TestSecretKey);
        secret.Login.Should().Be(TestLogin);
        secret.Password.Should().Be(TestPassword);
    }
}