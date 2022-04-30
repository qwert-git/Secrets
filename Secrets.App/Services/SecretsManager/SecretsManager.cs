using Secrets.App.Exceptions;
using Secrets.App.Models;
using Secrets.Services.SecretsManager.SecretsReader;
using Secrets.Services.SecretsManager.SecretsWriter;

namespace Secrets.Services.SecretsManager;

internal class SecretsManager : ISecretsManager
{
    private readonly ISecretsReader _secretsReader;
    private readonly ISecretsWriter _secretsWriter;

    public SecretsManager(ISecretsReader secretsReader, ISecretsWriter secretsWriter)
    {
        _secretsReader = secretsReader;
        _secretsWriter = secretsWriter;
    }

    public async Task<List<Secret>> GetAllAsync()
    {
		var allSecrets = await ReadSecretsInternalAsync();
        return allSecrets.ToList();
    }

    public async Task AddAsync(Secret secretToAdd)
    {
        var allSecrets = await GetAllAsync();

        allSecrets.Add(secretToAdd);

        await WriteSecretsInternalAsync(allSecrets);
    }

    public async Task RemoveSecretAsync(Secret secretToRemove)
    {
        var allSecrets = await GetAllAsync();

        // TODO: It's better to optimize. Different Secrets objects in memory.  
        var secretFromCollection = allSecrets.Find(s => s.Key == secretToRemove.Key);
        allSecrets.Remove(secretFromCollection);

        await WriteSecretsInternalAsync(allSecrets);
    }

	private async Task<ICollection<Secret>> ReadSecretsInternalAsync()
	{
		try
        {
            return await _secretsReader.ReadSecretsAsync();
        }
		catch(Exception ex)
		{
			throw new SecretsAppException($"Getting secrets failed with message `{ex.Message}`");
		}
	}

	private async Task WriteSecretsInternalAsync(IReadOnlyCollection<Secret> secrets)
	{
		try
		{
			await _secretsWriter.WriteAsync(secrets);
		}
		catch(Exception ex)
		{
			throw new SecretsAppException($"Saving secrets failed with message `{ex.Message}`");
		}
	}
}