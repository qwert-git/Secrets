using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Secrets.Services;

namespace Secrets
{
	internal static class Program
	{
		private static SymmetricDataEncryptor _encryptor;
		private static SingleRowSecretsParser _parser;

		// TODO: Move consts to appsettings
		private const string FilePath = @"C:\Users\AganurAhundov\OneDrive - Ladoburn Europe Ltd\Desktop\w\secrets.txt";
		private const string EncryptedFilePath = @"C:\Users\AganurAhundov\OneDrive - Ladoburn Europe Ltd\Desktop\w\secrets_enc.txt";

		private const string EncryptKey = "Xp2s5v8y/B?E(G+K";
		private const string RowSeparator = " ";

		public static async Task Main(string[] args)
		{
			_encryptor = new SymmetricDataEncryptor();
			_parser = new SingleRowSecretsParser(RowSeparator);

			if (args.Length == 1 && args[0] == "new")
			{
				await InitEncrypted();
			}

			var dataProvider = new FileDataProvider(EncryptedFilePath);
			var encryptedSecrets = await dataProvider.ReadAsync();
			
			var decryptedData = await _encryptor.DecryptAsync(EncryptKey,encryptedSecrets);
			var parsedSecrets = _parser.GetSecrets(decryptedData).ToArray();

			ShowSecrets(args, parsedSecrets);
		}

		private static async Task InitEncrypted()
		{
			var originDataProvider = new FileDataProvider(FilePath);
			var secretData = await originDataProvider.ReadAsync();

			var encryptedData = await _encryptor.EncryptAsync(EncryptKey, secretData);

			var encryptedDataProvider = new FileDataProvider(EncryptedFilePath);
			await encryptedDataProvider.WriteAsync(encryptedData);
		}
		
		private static void ShowSecrets(IReadOnlyList<string> args, IList<(string secretKey, string login, string pswrd)> parsedSecrets)
		{
			if (args.Count == 0)
			{
				for (var i = 0; i < parsedSecrets.Count; i++)
					Console.WriteLine($"{i + 1}. {parsedSecrets[i].secretKey}");
			}
			else
			{
				var (secretKey, login, pswrd) = parsedSecrets[int.Parse(args[0]) - 1];
				Console.WriteLine(secretKey is not null ? login + " " + pswrd : "Secret not found!");
			}
		}
	}
}