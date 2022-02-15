using System.Collections.Generic;

namespace Secrets.Services
{
	public interface ISecretsParser
	{
		ICollection<(string secretKey, string login, string pswrd)> GetSecrets(string rawData);
	}
}