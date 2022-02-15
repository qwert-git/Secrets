using System.IO;
using System.Threading.Tasks;
using Secrets.Core;

namespace Secrets.FileSystemIO
{
	public class FileDataWriter : IDataWriter
	{
		private readonly string _filePath;

		public FileDataWriter(string filePath)
		{
			_filePath = filePath;
		}

		/// <inheritdoc />
		public async Task WriteAsync(string data)
		{
			if (!File.Exists(_filePath))
			{
				await using var f = File.Create(_filePath);
			}

			await File.WriteAllTextAsync(_filePath, data);
		}
	}
}