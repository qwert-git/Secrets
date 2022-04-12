using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Secrets.App.Models;
using Secrets.Commands;
using Secrets.Services.SecretsManager;
using Xunit;

namespace Secrets.App.Tests.Commands;

public class RemoveSecretCommandTests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(99999)]
	public async Task SecretNumberOurOfRange_Should_ThrowArgumentException(int secretToRemoveNumber)
	{
		// Arrange
		var secrets = new Fixture().CreateMany<Secret>().ToList();
		
		var mockSecretsManager = new Mock<ISecretsManager>();
		mockSecretsManager
			.Setup(manager => manager.GetAllAsync())
			.ReturnsAsync(secrets);
		
		var command = new RemoveSecretCommand(secretToRemoveNumber, mockSecretsManager.Object);

		// Assert
		await Assert.ThrowsAsync<ArgumentException>(() => command.ExecuteAsync());
	}
	
	[Fact]
	public async Task ExecuteCommand_Should_PassRemoveSecretToManager()
	{
		// Arrange
		var secrets = new Fixture().CreateMany<Secret>().ToList();
		var secretToRemoveNumber = secrets.Count - 1;
		var secretToRemove = secrets[secretToRemoveNumber];
		
		var mockSecretsManager = new Mock<ISecretsManager>();
		mockSecretsManager
			.Setup(manager => manager.GetAllAsync())
			.ReturnsAsync(secrets);

		var command = new RemoveSecretCommand(secretToRemoveNumber, mockSecretsManager.Object);
		
		// Act
		await command.ExecuteAsync();
		
		// Assert
		mockSecretsManager.Verify(manager => 
				manager.RemoveSecretAsync(secretToRemove), 
			Times.Once);
	}
}