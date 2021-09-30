using System.Threading.Tasks;

namespace Secrets.Services
{
	public interface IDataProvider
	{
		Task<string> ReadAsync();

		Task WriteAsync(string data);
	}
}