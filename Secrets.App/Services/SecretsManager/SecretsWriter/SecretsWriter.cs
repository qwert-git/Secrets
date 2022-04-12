using Secrets.App.Models;
using Secrets.Core;
using Secrets.FileSystemIO;
using Secrets.Services.SecretsConverter;

namespace Secrets.Services.SecretsManager.SecretsWriter;

internal class SecretsWriter : ISecretsWriter
{
	private readonly ISecretsToRawDataConverter _rawDataConverter;
	private readonly IDataEncryptor _encryptor;
	private readonly IDataWriter _dataWriter;
	private readonly AppConfig _config;

	public SecretsWriter(ISecretsToRawDataConverter rawDataConverter, IDataEncryptor encryptor, IDataWriter dataWriter, AppConfig config)
	{
		_rawDataConverter = rawDataConverter;
		_encryptor = encryptor;
		_dataWriter = dataWriter;
		_config = config;
	}

	/// <inheritdoc />
	public async Task WriteAsync(IReadOnlyCollection<Secret> secrets)
	{
		var rawData = _rawDataConverter.GetRaw(secrets);

		var encryptedData = await _encryptor.EncryptAsync(_config.EncryptionKey, rawData);

		var encryptedDataWriter = new FileDataWriter(_config.EncryptedFilePath);
		await encryptedDataWriter.WriteAsync(encryptedData);
	}
}