using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Secrets.FileSystemIO;
using Secrets.Cryptography;
using Secrets.Services;

namespace Secrets
{
	internal static class Program
	{
		private static SymmetricDataEncryptor _encryptor;
		private static SymmetricDataDecryptor _decryptor;
		private static SingleRowSecretsParser _parser;

		// TODO: Move consts to appsettings
		private const string FilePath = @"C:\Users\AganurAhundov\OneDrive - Ladoburn Europe Ltd\Desktop\w\secrets.txt";
		private const string EncryptedFilePath = @"C:\Users\AganurAhundov\OneDrive - Ladoburn Europe Ltd\Desktop\w\secrets_enc.txt";

		private const string EncryptKey = "Xp2s5v8y/B?E(G+K";
		private const string RowSeparator = " ";

		public static async Task Main(string[] args)
		{
			_encryptor = new SymmetricDataEncryptor();
			_decryptor = new SymmetricDataDecryptor();
			_parser = new SingleRowSecretsParser(RowSeparator);

			if (IsNewCommand(args))
			{
				await InitEncrypted();
			}

			var dataReader = new FileDataReader(EncryptedFilePath);
			var encryptedSecrets = await dataReader.ReadAsync();
			
			var decryptedData = await _decryptor.DecryptAsync(EncryptKey,encryptedSecrets);
			var parsedSecrets = _parser.GetSecrets(decryptedData).ToArray();

			ShowSecrets(args, parsedSecrets);
		}

		private static async Task InitEncrypted()
		{
			var originDataReader = new FileDataReader(FilePath);
			var secretData = await originDataReader.ReadAsync();

			var encryptedData = await _encryptor.EncryptAsync(EncryptKey, secretData);

			var encryptedDataWriter = new FileDataWriter(EncryptedFilePath);
			await encryptedDataWriter.WriteAsync(encryptedData);
		}

		private static bool IsNewCommand(IReadOnlyList<string> args)
		{
			return args.Count == 1 && args[0] == "new";
		}

		private static void ShowSecrets(IReadOnlyList<string> args, IList<(string secretKey, string login, string pswrd)> parsedSecrets)
		{
			if (args.Count == 0 || IsNewCommand(args))
			{
				for (var i = 0; i < parsedSecrets.Count; i++)
					Console.WriteLine($"{i + 1}. {parsedSecrets[i].secretKey}");
			}
			else
			{
				var (secretKey, login, pswrd) = parsedSecrets[int.Parse(args[0]) - 1];
				Console.WriteLine(secretKey is not null ? $"{secretKey}: {login} {pswrd}" : "Secret not found!");
			}
		}
	}
}