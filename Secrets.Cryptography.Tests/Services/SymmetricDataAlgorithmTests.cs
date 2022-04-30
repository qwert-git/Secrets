using System.Threading.Tasks;
using Secrets.Cryptography;
using Xunit;

namespace Secrets.Tests.Services
{
	public class SymmetricDataAlgorithmTests
	{
		[Theory]
		[InlineData("any_secret_key", "string+to+decrypt")]
		[InlineData("b14ca5898a4e4133bbce2ea2315a1916", "string+to+decrypt")]
		public async Task BaseCase_Should_Encrypt_And_Decrypt_String(string key, string originalData)
		{
			var encryptor = new SymmetricDataEncryptor();
			var encryptedData = await encryptor.EncryptAsync(key, originalData);
	
			Assert.NotEqual(encryptedData, originalData);
	
			var decryptor = new SymmetricDataDecryptor();
			var decryptedData = await decryptor.DecryptAsync(key, encryptedData);
	
			Assert.Equal(decryptedData, originalData);
		}
	}
}