using Secrets.App.Services;
using Secrets.App.Services.Presenter;

namespace Secrets.App.Commands;

internal class ShowSecretCommand : ICommand
{
    private readonly int _secretNumber;
    private readonly SecretsProvider _secretsProvider;
    private readonly ConsolePresenter _presenter;

    public ShowSecretCommand(int secretNumber, SecretsProvider secretsProvider, ConsolePresenter presenter)
    {
        this._secretNumber = secretNumber;
        _secretsProvider = secretsProvider;
        _presenter = presenter;
    }

    public async Task ExecuteAsync()
    {
        var secrets = await _secretsProvider.GetAllAsync();
        
        _presenter.PresentSecret(secrets[_secretNumber]);
    }
}