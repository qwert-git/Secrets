using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Secrets.App.Commands;
using Secrets.App.Services;
using Secrets.App.Services.Configuration;
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
    public void CommandTypeDoesNotExists_Should_ThrowArgumentException()
    {
        // Arrange
        var args = new[] { "NotExistsCommand" };
        var commandFactory = new CommandsFactory(_serviceProvider, new ConsoleCommandTranslator(args));

        // Assert
        Assert.Throws<InvalidOperationException>(() => commandFactory.Make());
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
}