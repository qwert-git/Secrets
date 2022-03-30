using Microsoft.Extensions.DependencyInjection;
using Secrets;
using Secrets.Core;
using Secrets.FileSystemIO;
using Secrets.App.Services;
using Secrets.App.Services.Configuration;
using Secrets.App.Services.Presenter;

var config = ConsoleAppConfigProvider.GetConfig();
var serviceProvider = ConsoleAppServiceProvider.GetServiceProvider(config);
var commandTranslator = new ConsoleCommandTranslator(args);

var encryptor = serviceProvider.GetService<IDataEncryptor>() ??
                 throw new ApplicationException($"Cannot find dependency for {nameof(IDataEncryptor)}");
var decryptor = serviceProvider.GetService<IDataDecryptor>() ??
                 throw new ApplicationException($"Cannot find dependency for {nameof(IDataDecryptor)}");
var parser = serviceProvider.GetService<ISecretsParser>() ??
              throw new ApplicationException($"Cannot find dependency for {nameof(ISecretsParser)}");

if (commandTranslator.IsInitEncyrptionFile())
{
	await InitEncrypted(config);
}

var dataReader = new FileDataReader(config.EncryptedFilePath);
var encryptedSecrets = await dataReader.ReadAsync();

var decryptedData = await decryptor.DecryptAsync(config.EncryptionKey, encryptedSecrets);
var secrets = parser.GetSecrets(decryptedData).ToArray();

if (commandTranslator.IsShowAllSecrets() || commandTranslator.IsInitEncyrptionFile())
{
	ConsolePresenter.PresentAllKeys(secrets);
}
else
{
	var secretToShow = commandTranslator.GetSecret(secrets);
	ConsolePresenter.PresentSecret(secretToShow);
}

#region Application Methods
async Task InitEncrypted(AppConfig config)
{
	var originDataReader = new FileDataReader(config.OriginFilePath);
	var secretData = await originDataReader.ReadAsync();

	var encryptedData = await encryptor.EncryptAsync(config.EncryptionKey, secretData);

	var encryptedDataWriter = new FileDataWriter(config.EncryptedFilePath);
	await encryptedDataWriter.WriteAsync(encryptedData);
}
#endregion