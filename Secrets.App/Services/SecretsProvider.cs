using Secrets.App.Models;
using Secrets.Core;
using Secrets.FileSystemIO;

namespace Secrets.App.Services;

internal class SecretsProvider
{
    private IDataDecryptor _decryptor;
    private IDataEncryptor _encryptor;
    private ISecretsParser _parser;
    private AppConfig _config;

    public SecretsProvider(
        IDataDecryptor decryptor,
        IDataEncryptor encryptor,
        ISecretsParser parser,
        AppConfig config)
    {
        _decryptor = decryptor;
        _encryptor = encryptor;
        _parser = parser;
        _config = config;
    }

    public async Task<List<Secret>> GetAllAsync()
    {
        var secretsHashReader = new FileDataReader(_config.EncryptedFilePath);

        var encryptedSecrets = await secretsHashReader.ReadAsync();
        var rawSecrets = await _decryptor.DecryptAsync(_config.EncryptionKey, encryptedSecrets);

        return _parser.GetSecrets(rawSecrets).ToList();
    }

    public async Task InitEncryptedAsync()
    {
        var originDataReader = new FileDataReader(_config.OriginFilePath);
        var secretData = await originDataReader.ReadAsync();

        var encryptedData = await _encryptor.EncryptAsync(_config.EncryptionKey, secretData);

        var encryptedDataWriter = new FileDataWriter(_config.EncryptedFilePath);
        await encryptedDataWriter.WriteAsync(encryptedData);
    }
}