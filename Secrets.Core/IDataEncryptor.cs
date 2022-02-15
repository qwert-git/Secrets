using System.Threading.Tasks;

namespace Secrets.Core
{
	public interface IDataEncryptor
	{
		Task<string> EncryptAsync(string key, string data);
	}
}