using Microsoft.Extensions.DependencyInjection;
using Secrets;
using Secrets.Core;
using Secrets.FileSystemIO;
using Secrets.Services;
using Secrets.App.ConfigurationServices;

var config = ConsoleAppConfigProvider.GetConfig();
var serviceProvider = ConsoleAppServiceProvider.GetServiceProvider(config);

var encryptor = serviceProvider.GetService<IDataEncryptor>() ??
                 throw new ApplicationException($"Cannot find dependency for {nameof(IDataEncryptor)}");
var decryptor = serviceProvider.GetService<IDataDecryptor>() ??
                 throw new ApplicationException($"Cannot find dependency for {nameof(IDataDecryptor)}");
var parser = serviceProvider.GetService<ISecretsParser>() ??
              throw new ApplicationException($"Cannot find dependency for {nameof(ISecretsParser)}");

if (IsNewCommand(args))
{
	await InitEncrypted(config);
}

var dataReader = new FileDataReader(config.EncryptedFilePath);
var encryptedSecrets = await dataReader.ReadAsync();

var decryptedData = await decryptor.DecryptAsync(config.EncryptionKey, encryptedSecrets);
var parsedSecrets = parser.GetSecrets(decryptedData).ToArray();

ShowSecrets(args, parsedSecrets);

#region Application Methods
async Task InitEncrypted(AppConfig config)
{
	var originDataReader = new FileDataReader(config.OriginFilePath);
	var secretData = await originDataReader.ReadAsync();

	var encryptedData = await encryptor.EncryptAsync(config.EncryptionKey, secretData);

	var encryptedDataWriter = new FileDataWriter(config.EncryptedFilePath);
	await encryptedDataWriter.WriteAsync(encryptedData);
}

static bool IsNewCommand(IReadOnlyList<string> args)
{
	return args.Count == 1 && args[0] == "new";
}

static void ShowSecrets(IReadOnlyList<string> args,
	IList<(string secretKey, string login, string pswrd)> parsedSecrets)
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

#endregion