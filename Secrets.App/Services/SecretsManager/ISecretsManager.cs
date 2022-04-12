using Secrets.App.Models;

namespace Secrets.Services.SecretsManager;

internal interface ISecretsManager
{
	Task<List<Secret>> GetAllAsync();

	Task AddAsync(Secret secretToAdd);

	Task InitEncryptedAsync();

	Task RemoveSecretAsync(Secret secretToRemove);
}