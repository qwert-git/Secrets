using System.Security.Cryptography;

namespace Secrets.Cryptography
{
	public abstract class SymetricCryptographyBase
	{
		private static readonly byte[] Salt =
			{ 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
		
		protected static Aes GetAlgorithm(string key)
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