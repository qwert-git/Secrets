using Microsoft.Extensions.DependencyInjection;
using Secrets.App.Services;
using Secrets.App.Services.Presenter;
using Secrets.App.Services.SecretsParser;
using Secrets.App.Services.SecretsToRawDataConverter;
using Secrets.Commands;
using Secrets.Core;
using Secrets.Cryptography;
using Secrets.FileSystemIO;
using Secrets.Services.CommandTranslator;
using Secrets.Services.SecretsConverter;
using Secrets.Services.SecretsManager;
using Secrets.Services.SecretsManager.SecretsReader;
using Secrets.Services.SecretsManager.SecretsWriter;

namespace Secrets.Services.Configuration;

internal static class ConsoleAppServiceProvider
{
    public static ServiceProvider GetServiceProvider(AppConfig config, IReadOnlyList<string> args)
    {
        var encryptedFileName = AppContext.BaseDirectory + "/encrypted.json";
        return new ServiceCollection()
            .AddSingleton(config)
            .AddSecretsReader(encryptedFileName)
            .AddSecretsWriter(encryptedFileName)
            .AddSingleton<ISecretsManager, SecretsManager.SecretsManager>()
            .AddSingleton<ICommandTranslator>(_ => new ConsoleCommandTranslator(args))
            .AddSingleton<ConsolePresenter>()
            .AddSingleton<ShowAllSecretsCommand>()
            .BuildServiceProvider();
    }

    private static IServiceCollection AddSecretsReader(this IServiceCollection services, string encryptedFileName)
    {
        return services
            .AddSingleton<IDataReader>(_ => new FileDataReader(encryptedFileName))
            .AddSingleton<IDataDecryptor, SymmetricDataDecryptor>()
            .AddSingleton<ISecretsParser, JsonSecretsParser>()
            .AddSingleton<ISecretsReader, SecretsReader>();
    }
    
    private static IServiceCollection AddSecretsWriter(this IServiceCollection services, string encryptedFileName)
    {
        return services
            .AddSingleton<IDataWriter>(_ => new FileDataWriter(encryptedFileName))
            .AddSingleton<IDataEncryptor, SymmetricDataEncryptor>()
            .AddSingleton<ISecretsToRawDataConverter, SecretsToJsonConverter>()
            .AddSingleton<ISecretsWriter, SecretsWriter>();
    }
}