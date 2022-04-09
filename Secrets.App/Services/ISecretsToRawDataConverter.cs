using Secrets.App.Models;

namespace Secrets.App.Services;

internal interface ISecretsToRawDataConverter
{
	string GetRaw(ICollection<Secret> secrets);
}
