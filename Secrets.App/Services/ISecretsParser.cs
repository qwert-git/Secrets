namespace Secrets.App.Services;

internal interface ISecretsParser
{
	ICollection<(string secretKey, string login, string pswrd)> GetSecrets(string rawData);
}