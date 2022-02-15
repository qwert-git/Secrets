using System.IO;
using System.Threading.Tasks;
using Secrets.Core;

namespace Secrets.FileSystemIO
{
	public class FileDataReader : IDataReader
	{
		private readonly string _filePath;

		public FileDataReader(string filePath)
		{
			_filePath = filePath;
		}

		/// <inheritdoc />
		public Task<string> ReadAsync()
		{
			return File.ReadAllTextAsync(_filePath);
		}
	}
}