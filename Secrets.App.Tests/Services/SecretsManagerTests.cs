using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Secrets.App.Models;
using Secrets.Services.SecretsManager;
using Secrets.Services.SecretsManager.SecretsReader;
using Secrets.Services.SecretsManager.SecretsWriter;
using Xunit;

namespace Secrets.App.Tests.Services;

public class SecretsProviderTests
{
	[Fact]
	public async Task RemoveSecret_Should_Be_NoSecretInSavingCollection()
	{
		// Assert
		var secrets = new Fixture().CreateMany<Secret>();
		var secretToRemove = secrets.Last();

		var mockSecretsReader = new Mock<ISecretsReader>();
		var mockSecretsWriter = new Mock<ISecretsWriter>();

		var secretsProvider = new SecretsManager(mockSecretsReader.Object, mockSecretsWriter.Object);

		// Act
		await secretsProvider.RemoveSecretAsync(secretToRemove);

		// Arrange
		mockSecretsWriter.Verify(writer =>
				writer.WriteAsync(
					It.Is<IReadOnlyCollection<Secret>>(ss => !ss.Contains(secretToRemove))),
			Times.Once);
	}
}