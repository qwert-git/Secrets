using Microsoft.Extensions.DependencyInjection;
using Secrets.App.Commands;
using Secrets.App.Exceptions;
using Secrets.App.Services.Presenter;
using Secrets.Services.CommandTranslator;
using Secrets.Services.SecretsManager;

namespace Secrets.Commands.Factory;

internal class CommandsFactory
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ICommandTranslator _commandTranslator;

	public CommandsFactory(IServiceProvider serviceProvider, ICommandTranslator commandTranslator)
	{
		_serviceProvider = serviceProvider;
		_commandTranslator = commandTranslator;
	}

	public ICommand Make()
	{
		if (_commandTranslator.IsShowAllSecrets())
			return _serviceProvider.GetService<ShowAllSecretsCommand>();
		if (_commandTranslator.IsGetSecret())
		{
			return new ShowSecretCommand(
				_commandTranslator.GetAddKeyNumber(),
				_serviceProvider.GetRequiredService<ISecretsManager>(),
				_serviceProvider.GetRequiredService<ConsolePresenter>());
		}

		if (_commandTranslator.IsAddNew())
		{
			var secretToAdd = _commandTranslator.GetNewSecret();
			return new AddSecretCommand(
				secretToAdd, 
				_serviceProvider.GetRequiredService<ISecretsManager>());
		}

		if (_commandTranslator.IsRemove())
		{
			return new RemoveSecretCommand(
				_commandTranslator.GetRemoveKeyNumber(),
				_serviceProvider.GetRequiredService<ISecretsManager>()
			);
		}

		throw new SecretsAppException("Undefined command.");
	}
}