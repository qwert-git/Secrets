using System.Threading.Tasks;

namespace Secrets.Core
{
	public interface IDataReader
	{
		Task<string> ReadAsync();
	}
}