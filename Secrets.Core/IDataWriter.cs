using System.Threading.Tasks;

namespace Secrets.Core
{
	public interface IDataWriter
	{
		Task WriteAsync(string data);
	}
}