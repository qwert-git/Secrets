using System;
using FluentAssertions;
using Secrets.App.Exceptions;
using Secrets.Services.CommandTranslator;
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
	public void GetNewSecret_NoArguments_Should_ThrowException(string[] args, bool _)
	{
		// Arrange
		var translator = new ConsoleCommandTranslator(args);

		// Assert
		Assert.Throws<SecretsAppException>(() => translator.GetNewSecret());
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

	[Theory]
	[InlineData(new[] { "" }, false)]
	[InlineData(new[] { "wrong_command" }, false)]
	[InlineData(new[] { "rm" }, false)]
	[InlineData(new[] { "remove" }, false)]
	[InlineData(new[] { "1", "rm" }, false)]
	[InlineData(new[] { "1", "remove" }, false)]
	[InlineData(new[] { "rm", "1" }, true)]
	[InlineData(new[] { "remove", "1" }, true)]
	public void RemoveCommand_CommandName_Should_ReturnExpectedResult(string[] args, bool expectedResult)
	{
		// Arrange
		var translator = new ConsoleCommandTranslator(args);

		// Act
		var result = translator.IsRemove();

		// Assert
		result.Should().Be(expectedResult);
	}

	[Fact]
	public void GetKeyNumber_KeyExists_Should_ReturnKey()
	{
		// Arrange
		const int testKeyNumber = 2;
		var args = new[] { testKeyNumber.ToString() };
		var translator = new ConsoleCommandTranslator(args);

		// Act
		var result = translator.GetAddKeyNumber();

		// Assert
		result.Should().Be(testKeyNumber - 1);
	}
}