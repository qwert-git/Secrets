using Secrets.App.Models;

namespace Secrets.App.Services;

internal interface ISecretsParser
{
	ICollection<Secret> GetSecrets(string rawData);
}