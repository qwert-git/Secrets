using Secrets.App.Models;
using Secrets.App.Services;

namespace Secrets.App.Commands;
internal class AddSecretCommand : ICommand
{
    private readonly Secret _secretToAdd;
    private readonly ISecretsProvider _secretsProvider;

    public AddSecretCommand(Secret secretToAdd, ISecretsProvider secretsProvider)
    {
        _secretToAdd = secretToAdd ?? throw new ArgumentNullException(nameof(secretToAdd));
        _secretsProvider = secretsProvider;
    }

    public Task ExecuteAsync()
    {
        return _secretsProvider.AddAsync(_secretToAdd);
    }
}