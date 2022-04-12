using Secrets.App.Commands;
using Secrets.App.Models;
using Secrets.Services.SecretsManager;

namespace Secrets.Commands;

internal class AddSecretCommand : ICommand
{
    private readonly Secret _secretToAdd;
    private readonly ISecretsManager _secretsManager;

    public AddSecretCommand(Secret secretToAdd, ISecretsManager secretsManager)
    {
        _secretToAdd = secretToAdd ?? throw new ArgumentNullException(nameof(secretToAdd));
        _secretsManager = secretsManager;
    }

    public Task ExecuteAsync()
    {
        return _secretsManager.AddAsync(_secretToAdd);
    }
}