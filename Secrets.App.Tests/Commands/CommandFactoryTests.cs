using System;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Secrets.App.Exceptions;
using Secrets.App.Models;
using Secrets.App.Services.Configuration;
using Secrets.Commands;
using Secrets.Commands.Factory;
using Secrets.Services.CommandTranslator;
using Secrets.Services.Configuration;
using Xunit;

namespace Secrets.App.Tests.Commands;

public class CommandFactoryTests
{
	private readonly ServiceProvider _serviceProvider;

	public CommandFactoryTests()
	{
		_serviceProvider = ConsoleAppServiceProvider.GetServiceProvider(new AppConfig(), Array.Empty<string>());
	}

	[Fact]
	public void CommandTypeDoesNotExists_Should_ThrowException()
	{
		// Arrange
		var args = new[] { "NotExistsCommand" };
		var commandFactory = new CommandsFactory(_serviceProvider, new ConsoleCommandTranslator(args));

		// Assert
		Assert.Throws<SecretsAppException>(() => commandFactory.Make());
	}

	[Fact]
	public void GetShowAllCommand_Should_ReturnCommandOfCorrectType()
	{
		// Arrange
		var args = new[] { string.Empty };
		var commandFactory = new CommandsFactory(_serviceProvider, new ConsoleCommandTranslator(args));

		// Act
		var command = commandFactory.Make();

		// Assert
		command.Should().BeOfType<ShowAllSecretsCommand>();
	}

	[Fact]
	public void GetShowCommand_Should_ReturnCommandOfCorrectType()
	{
		// Arrange
		const string secretNumberArg = "2";
		var args = new string[] { secretNumberArg };

		var serviceProvider = ConsoleAppServiceProvider.GetServiceProvider(new AppConfig(), args);
		var commandFactory = new CommandsFactory(serviceProvider, new ConsoleCommandTranslator(args));

		// Act
		var command = commandFactory.Make();

		// Assert
		command.Should().BeOfType<ShowSecretCommand>();
	}

	[Fact]
	public void AddSecretCommand_Should_ReturnCommandOfCorrectType()
	{
		// Arrange
		var mockCommandTranslator = new Mock<ICommandTranslator>();
		mockCommandTranslator
			.Setup(commandTranslator => commandTranslator.IsAddNew())
			.Returns(true);
		mockCommandTranslator
			.Setup(commandTranslator => commandTranslator.GetNewSecret())
			.Returns(new Fixture().Create<Secret>());

		var serviceProvider =
			ConsoleAppServiceProvider.GetServiceProvider(new AppConfig(), args: Array.Empty<string>());
		var commandFactory = new CommandsFactory(serviceProvider, mockCommandTranslator.Object);

		// Act
		var command = commandFactory.Make();

		// Assert
		command.Should().BeOfType<AddSecretCommand>();
	}

	[Fact]
	public void RemoveSecretCommand_Should_ReturnCommandOfCorrectType()
	{
		// Arrange
		var mockCommandTranslator = new Mock<ICommandTranslator>();
		mockCommandTranslator
			.Setup(commandTranslator => commandTranslator.IsRemove())
			.Returns(true);
		mockCommandTranslator
			.Setup(commandTranslator => commandTranslator.GetAddKeyNumber())
			.Returns(It.IsAny<int>());

		var serviceProvider =
			ConsoleAppServiceProvider.GetServiceProvider(new AppConfig(), args: Array.Empty<string>());
		var commandFactory = new CommandsFactory(serviceProvider, mockCommandTranslator.Object);

		// Act
		var command = commandFactory.Make();

		// Assert
		command.Should().BeOfType<RemoveSecretCommand>();
	}
}