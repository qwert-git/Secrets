using Secrets.App.Commands;
using Secrets.App.Services.Presenter;
using Secrets.Services.SecretsManager;

namespace Secrets.Commands;

internal class ShowSecretCommand : ICommand
{
    private readonly int _secretNumber;
    private readonly ISecretsManager _secretsManager;
    private readonly ConsolePresenter _presenter;

    public ShowSecretCommand(int secretNumber, ISecretsManager secretsManager, ConsolePresenter presenter)
    {
        this._secretNumber = secretNumber;
        _secretsManager = secretsManager;
        _presenter = presenter;
    }

    public async Task ExecuteAsync()
    {
        var secrets = await _secretsManager.GetAllAsync();
        
        _presenter.PresentSecret(secrets[_secretNumber]);
    }
}