using Secrets.App.Commands;
using Secrets.App.Exceptions;
using Secrets.App.Models;
using Secrets.Services.SecretsManager;

namespace Secrets.Commands;

internal class AddSecretCommand : ICommand
{
    private readonly Secret _secretToAdd;
    private readonly ISecretsManager _secretsManager;

    public AddSecretCommand(Secret secretToAdd, ISecretsManager secretsManager)
    {
        _secretToAdd = secretToAdd ?? throw new SecretsAppException("Secret to add is null.");
        _secretsManager = secretsManager;
    }

    public Task ExecuteAsync()
    {
        return _secretsManager.AddAsync(_secretToAdd);
    }
}