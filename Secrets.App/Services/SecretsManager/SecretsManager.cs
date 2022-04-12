using System.Collections.Immutable;
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
		var allSecrets = await _secretsReader.ReadSecretsAsync();
		return allSecrets.ToList();
	}

	public async Task InitEncryptedAsync()
	{
		var allSecrets = await _secretsReader.ReadSecretsAsync();
		await _secretsWriter.WriteAsync(allSecrets.ToImmutableArray());
	}

	public async Task AddAsync(Secret secretToAdd)
	{
		var allSecrets = await GetAllAsync();

		allSecrets.Add(secretToAdd);

		await _secretsWriter.WriteAsync(allSecrets);
	}

	public async Task RemoveSecretAsync(Secret secretToRemove)
	{
		var allSecrets = await GetAllAsync();

		// TODO: It's better to optimize. Different Secrets objects in memory.  
		var secretFromCollection = allSecrets.Find(s => s.Key == secretToRemove.Key);
		allSecrets.Remove(secretFromCollection);

		await _secretsWriter.WriteAsync(allSecrets);
	}
}