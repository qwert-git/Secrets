using Microsoft.Extensions.Configuration;

namespace Secrets.App.Services.Configuration;

internal static class ConsoleAppConfigProvider
{
    public static AppConfig GetConfig()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .Build()
            .Get<AppConfig>();
    }
}