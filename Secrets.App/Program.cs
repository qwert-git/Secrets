using Microsoft.Extensions.DependencyInjection;
using Secrets.Core;
using Secrets.App.Services;
using Secrets.App.Services.Configuration;
using Secrets.App.Commands;

var config = ConsoleAppConfigProvider.GetConfig();
var serviceProvider = ConsoleAppServiceProvider.GetServiceProvider(config, args);
var commandTranslator = serviceProvider.GetService<ICommandTranslator>() ??
				throw new ApplicationException($"Cannot find dependency for {nameof(ConsoleCommandTranslator)}");
var secretsProvider = serviceProvider.GetService<SecretsProvider>() ??
				throw new ApplicationException($"Cannot find dependency for {nameof(IDataEncryptor)}");
var commandsFactory = new CommandsFactory(serviceProvider, commandTranslator);


if (commandTranslator.IsInitEncyrptionFile())
{
	await secretsProvider.InitEncryptedAsync();
}

var command = commandsFactory.Make();
await command.ExecuteAsync();