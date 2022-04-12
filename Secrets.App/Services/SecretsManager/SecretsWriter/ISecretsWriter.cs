using Secrets.App.Models;

namespace Secrets.Services.SecretsManager.SecretsWriter;

internal interface ISecretsWriter
{
	public Task WriteAsync(IReadOnlyCollection<Secret> secrets);
}