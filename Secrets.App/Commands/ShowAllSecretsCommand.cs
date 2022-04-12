using Secrets.App.Commands;
using Secrets.App.Services.Presenter;
using Secrets.Services.SecretsManager;

namespace Secrets.Commands;

internal class ShowAllSecretsCommand : ICommand
{
    private readonly ISecretsManager _secretsManager;
    private readonly ConsolePresenter _presenter;

    public ShowAllSecretsCommand(ISecretsManager secretsProver, ConsolePresenter presenter)
    {
        _secretsManager = secretsProver;
        _presenter = presenter;
    }

    public async Task ExecuteAsync()
    {
        var secrets = await _secretsManager.GetAllAsync();
        _presenter.PresentAllKeys(secrets);
    }
}
