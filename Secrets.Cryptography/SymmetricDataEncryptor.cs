using System.Security.Cryptography;
using Secrets.Core;

namespace Secrets.Cryptography
{
	public class SymmetricDataEncryptor : SymetricCryptographyBase, IDataEncryptor
	{
		public async Task<string> EncryptAsync(string key, string text)
		{
			using var aes = GetAlgorithm(key);

			var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

			await using var memoryStream = new MemoryStream();
			await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			await using (var streamWriter = new StreamWriter(cryptoStream))
			{
				await streamWriter.WriteAsync(text);
			}

			var array = memoryStream.ToArray();

			return Convert.ToBase64String(array);
		}
	}
}