using Microsoft.Extensions.DependencyInjection;
using Secrets.Core;
using Secrets.Services;
using Secrets.Cryptography;

namespace Secrets.App.ConfigurationServices;

internal static class ConsoleAppServiceProvider
{
    public static ServiceProvider GetServiceProvider(AppConfig config)
    {
        return new ServiceCollection()
            .AddSingleton<AppConfig>(config)
            .AddSingleton<IDataEncryptor, SymmetricDataEncryptor>()
            .AddSingleton<IDataDecryptor, SymmetricDataDecryptor>()
            .AddSingleton<ISecretsParser, SingleRowSecretsParser>(provider => new SingleRowSecretsParser(config.RowSeparator))
            .BuildServiceProvider();
    }
}