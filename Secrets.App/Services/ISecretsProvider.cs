using Secrets.App.Models;

namespace Secrets.App.Services;
internal interface ISecretsProvider
{
    Task<List<Secret>> GetAllAsync();

    Task AddAsync(Secret secretToAdd);

    Task InitEncryptedAsync();
}
