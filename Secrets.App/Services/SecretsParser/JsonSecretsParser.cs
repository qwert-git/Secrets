using Secrets.App.Exceptions;
using Secrets.App.Models;
using Secrets.App.Utils;

namespace Secrets.App.Services.SecretsParser;

internal class JsonSecretsParser : ISecretsParser
{
    public ICollection<Secret> GetSecrets(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
            return Array.Empty<Secret>();

        return GetSecretsInternal(rawData);
    }

    private ICollection<Secret> GetSecretsInternal(string rawData)
    {
        try
        {
            return CustomJsonConverter.Deserialize<ICollection<Secret>>(rawData);
        }
        catch (SecretsAppJsonConverterException ex)
        {
            throw new SecretsAppException($"Parsing from the raw format failed with message `{ex.Message}`");
        }
    }
}