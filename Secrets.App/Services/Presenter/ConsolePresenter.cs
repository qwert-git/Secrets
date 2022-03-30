using Secrets.App.Models;

namespace Secrets.App.Services.Presenter;

internal static class ConsolePresenter
{
    public static void PresentAllKeys(IReadOnlyList<Secret> secrets)
    {
        for (var i = 0; i < secrets.Count; i++)
			Console.WriteLine($"{i + 1}. {secrets[i].Key}");
    }

    public static void PresentSecret(Secret secret)
    {
        Console.WriteLine(secret is not null ? $"{secret.Key}: {secret.Login} {secret.Password}" : "Secret not found!");
    }
}