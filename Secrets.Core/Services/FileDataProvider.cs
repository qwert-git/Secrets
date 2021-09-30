using System.IO;
using System.Threading.Tasks;

namespace Secrets.Services
{
	internal class FileDataProvider : IDataProvider
	{
		private readonly string _filePath;

		public FileDataProvider(string filePath)
		{
			_filePath = filePath;
		}

		/// <inheritdoc />
		public Task<string> ReadAsync()
		{
			return File.ReadAllTextAsync(_filePath);
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