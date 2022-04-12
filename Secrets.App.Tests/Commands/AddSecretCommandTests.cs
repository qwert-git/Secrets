using System;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Secrets.App.Commands;
using Secrets.App.Models;
using Secrets.App.Services;
using Secrets.Services.SecretsManager;
using Secrets.Services.SecretsProvider;
using Xunit;

namespace Secrets.App.Tests.Commands;
public class AddSecretCommandTests
{
    private readonly Mock<ISecretsManager> _mockSecretsProvider;

    public AddSecretCommandTests()
    {
        _mockSecretsProvider = new Mock<ISecretsManager>();
    }

    [Fact]
    public void SecretToAdd_IsNull_Should_ThrowArgumentNullException()
    {
        // Arrange
        var secretToAdd = null as Secret;

        // Assert
        Assert.Throws<ArgumentNullException>(() => new AddSecretCommand(secretToAdd, _mockSecretsProvider.Object));
    }

    [Fact]
    public async Task AddSecrets_Should_InvokeSecretsProvider()
    {
        // Arrange
        var secretToAdd = new Fixture().Create<Secret>();
        var command = new AddSecretCommand(secretToAdd, _mockSecretsProvider.Object);

        // Act
        await command.ExecuteAsync();

        // Assert
        _mockSecretsProvider.Verify(secretProvider => secretProvider.AddAsync(secretToAdd), Times.Once);
    }
}