using System.Threading.Tasks;

namespace Secrets.Core
{
	public interface IDataDecryptor
	{
		Task<string> DecryptAsync(string key, string data);
	}
}