using Secrets.App.Models;

namespace Secrets.Services.SecretsConverter;

internal interface ISecretsToRawDataConverter
{
	string GetRaw(IEnumerable<Secret> secrets);
}
