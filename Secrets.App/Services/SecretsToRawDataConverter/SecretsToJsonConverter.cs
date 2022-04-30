using Secrets.App.Models;
using Secrets.App.Utils;
using Secrets.Services.SecretsConverter;

namespace Secrets.App.Services.SecretsToRawDataConverter;

internal class SecretsToJsonConverter : ISecretsToRawDataConverter
{
    public string GetRaw(IEnumerable<Secret> secrets)
    {
        if(secrets is null || !secrets.Any())
            return string.Empty;
        
        return CustomJsonConverter.Serialize(secrets);
    }
}