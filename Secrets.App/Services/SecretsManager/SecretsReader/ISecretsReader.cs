using Secrets.App.Models;

namespace Secrets.Services.SecretsManager.SecretsReader;

internal interface ISecretsReader
{
	public Task<ICollection<Secret>> ReadSecretsAsync();
}