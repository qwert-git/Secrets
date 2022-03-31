using Microsoft.Extensions.DependencyInjection;
using Secrets.Core;
using Secrets.App.Services;
using Secrets.App.Services.Configuration;
using Secrets.App.Services.Presenter;

var config = ConsoleAppConfigProvider.GetConfig();
var serviceProvider = ConsoleAppServiceProvider.GetServiceProvider(config);
var commandTranslator = new ConsoleCommandTranslator(args);
var secretsProvider = serviceProvider.GetService<SecretsProvider>() ??
				throw new ApplicationException($"Cannot find dependency for {nameof(IDataEncryptor)}");


if (commandTranslator.IsInitEncyrptionFile())
{
	await secretsProvider.InitEncryptedAsync();
}

var secrets = await secretsProvider.GetAllAsync();
if (commandTranslator.IsShowAllSecrets() || commandTranslator.IsInitEncyrptionFile())
{
	ConsolePresenter.PresentAllKeys(secrets);
}
else
{
	var secretToShow = commandTranslator.GetSecret(secrets);
	ConsolePresenter.PresentSecret(secretToShow);
}