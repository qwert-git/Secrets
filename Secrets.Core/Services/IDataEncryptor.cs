using System.Threading.Tasks;

namespace Secrets.Services
{
	public interface IDataEncryptor
	{
		Task<string> EncryptAsync(string key, string data);

		Task<string> DecryptAsync(string key, string encryptedData);
	}
}