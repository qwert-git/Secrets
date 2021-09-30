using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Secrets.Services
{
	public class SymmetricDataEncryptor : IDataEncryptor
	{
		private static readonly byte[] Salt =
			{ 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

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

		private static Aes GetAlgorithm(string key)
		{
			var aes = Aes.Create();
			var pdb = new Rfc2898DeriveBytes(key, Salt);
			aes.Key = pdb.GetBytes(32);
			aes.IV = pdb.GetBytes(16);
			aes.Padding = PaddingMode.PKCS7;

			return aes;
		}
	}
}