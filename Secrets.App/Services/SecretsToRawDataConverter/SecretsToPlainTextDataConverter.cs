using System.Text;
using Secrets.App.Models;

namespace Secrets.Services.SecretsConverter;

internal class SecretsToPlainTextDataConverter : ISecretsToRawDataConverter
{
    public string GetRaw(IEnumerable<Secret> secrets)
    {
        var sb = new StringBuilder();
        foreach(var secret in secrets)
        {
            sb.AppendLine($"{secret.Key} {secret.Login} {secret.Password}");
        }

        return sb.ToString().TrimEnd();
    }
}