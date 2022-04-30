using Microsoft.Extensions.DependencyInjection;
using Secrets.App.Exceptions;
using Secrets.App.Services.Configuration;
using Secrets.Commands.Factory;
using Secrets.Services.CommandTranslator;
using Secrets.Services.Configuration;
using Secrets.Services.SecretsManager;

var config = ConsoleAppConfigProvider.GetConfig();
var serviceProvider = ConsoleAppServiceProvider.GetServiceProvider(config, args);
var commandTranslator = serviceProvider.GetService<ICommandTranslator>() ??
                throw new ApplicationException($"Cannot find dependency for {nameof(ICommandTranslator)}");
var secretsProvider = serviceProvider.GetService<ISecretsManager>() ??
                throw new ApplicationException($"Cannot find dependency for {nameof(ISecretsManager)}");
var commandsFactory = new CommandsFactory(serviceProvider, commandTranslator);


try
{
    var command = commandsFactory.Make();
    await command.ExecuteAsync();
}
catch (SecretsAppException exception)
{
    Console.WriteLine(exception.Message);
}