using Microsoft.Extensions.DependencyInjection;
using Secrets.App.Commands;
using Secrets.App.Services.Presenter;
using Secrets.Core;
using Secrets.Cryptography;

namespace Secrets.App.Services.Configuration;

internal static class ConsoleAppServiceProvider
{
    public static ServiceProvider GetServiceProvider(AppConfig config, IReadOnlyList<string> args)
    {
        return new ServiceCollection()
            .AddSingleton<AppConfig>(config)
            .AddSingleton<IDataEncryptor, SymmetricDataEncryptor>()
            .AddSingleton<IDataDecryptor, SymmetricDataDecryptor>()
            .AddSingleton<ISecretsParser, SingleRowSecretsParser>(provider => new SingleRowSecretsParser(config.RowSeparator))
            .AddSingleton<SecretsProvider>()
            .AddSingleton<ISecretsToRawDataConverter, SecretsToPlainTextDataConverter>()
            .AddSingleton<ICommandTranslator>(provider => new ConsoleCommandTranslator(args))
            .AddSingleton<ConsolePresenter>()
            .AddSingleton<ShowAllSecretsCommand>()
            .BuildServiceProvider();
    }
}