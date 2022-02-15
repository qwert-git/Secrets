using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Secrets.Core;

namespace Secrets.Cryptography
{
	public class SymmetricDataDecryptor : SymetricCryptographyBase, IDataDecryptor
	{
		/// <inheritdoc />
		public async Task<string> DecryptAsync(string key, string encryptedData)
		{
			using var aes = GetAlgorithm(key);

			var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
			var buffer = Convert.FromBase64String(encryptedData);

			await using var memoryStream = new MemoryStream(buffer);
			await using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			using var streamReader = new StreamReader(cryptoStream);

			return await streamReader.ReadToEndAsync();
		}
	}
}