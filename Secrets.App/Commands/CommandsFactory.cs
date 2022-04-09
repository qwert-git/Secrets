using Microsoft.Extensions.DependencyInjection;
using Secrets.App.Services;
using Secrets.App.Services.Presenter;

namespace Secrets.App.Commands;
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
        if(_commandTranslator.IsShowAllSecrets())
            return _serviceProvider.GetService<ShowAllSecretsCommand>();
        else if(_commandTranslator.IsGetSecret())
        {
            return new ShowSecretCommand(
                    _commandTranslator.GetKeyNumber(),
                    _serviceProvider.GetRequiredService<SecretsProvider>(),
                    _serviceProvider.GetRequiredService<ConsolePresenter>());
        }
        else if(_commandTranslator.IsAddNew())
        {
            var secretToAdd = _commandTranslator.GetNewSecret();
            return new AddSecretCommand(secretToAdd, _serviceProvider.GetRequiredService<SecretsProvider>());
        }
        else
            throw new InvalidOperationException("Undefiend command."); 
    }
}