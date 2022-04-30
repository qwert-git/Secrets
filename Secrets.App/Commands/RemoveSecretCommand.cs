using Secrets.App.Commands;
using Secrets.App.Exceptions;
using Secrets.Services.SecretsManager;

namespace Secrets.Commands;

internal class RemoveSecretCommand : ICommand
{
	private readonly int _secretNumber;
	private readonly ISecretsManager _secretsManager;

	public RemoveSecretCommand(int secretNumber, ISecretsManager secretsManager)
	{
		_secretsManager = secretsManager;
		_secretNumber = secretNumber;
	}

	/// <inheritdoc />
	public async Task ExecuteAsync()
	{
		var allSecrets = await _secretsManager.GetAllAsync();
		if (_secretNumber > allSecrets.Count || _secretNumber <= 0)
			throw new SecretsAppException($"There is no secret with index {_secretNumber}");

		var secretToRemove = allSecrets[_secretNumber];
		await _secretsManager.RemoveSecretAsync(secretToRemove);
	}
}