using Secrets.App.Models;

namespace Secrets.Services.SecretsManager;

internal interface ISecretsManager
{
	Task<List<Secret>> GetAllAsync();

	Task AddAsync(Secret secretToAdd);

	Task RemoveSecretAsync(Secret secretToRemove);
}