using Secrets.App.Services;
using Secrets.App.Services.Presenter;

namespace Secrets.App.Commands;

internal class ShowAllSecretsCommand : ICommand
{
    private readonly SecretsProvider _secretsProvider;
    private readonly ConsolePresenter _presenter;

    public ShowAllSecretsCommand(SecretsProvider secretsProver, ConsolePresenter presenter)
    {
        _secretsProvider = secretsProver;
        _presenter = presenter;
    }

    public async Task ExecuteAsync()
    {
        var secrets = await _secretsProvider.GetAllAsync();
        _presenter.PresentAllKeys(secrets);
    }
}
