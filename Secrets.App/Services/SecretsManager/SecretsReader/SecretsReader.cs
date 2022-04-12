using Secrets.App.Models;
using Secrets.App.Services;
using Secrets.Core;

namespace Secrets.Services.SecretsManager.SecretsReader;

internal class SecretsReader : ISecretsReader
{
	private readonly IDataReader _secretsHashDataReader;
	private readonly IDataDecryptor _decryptor;
	private readonly ISecretsParser _secretsParser;
	private readonly AppConfig _config;

	public SecretsReader(IDataReader secretsHashDataReader, IDataDecryptor decryptor, ISecretsParser secretsParser, AppConfig config)
	{
		_secretsHashDataReader = secretsHashDataReader;
		_decryptor = decryptor;
		_config = config;
		_secretsParser = secretsParser;
	}

	/// <inheritdoc />
	public async Task<ICollection<Secret>> ReadSecretsAsync()
	{
		var encryptedSecrets = await _secretsHashDataReader.ReadAsync();
		var rawSecretsData = await _decryptor.DecryptAsync(_config.EncryptionKey, encryptedSecrets);
		return _secretsParser.GetSecrets(rawSecretsData).ToList();
	}
}