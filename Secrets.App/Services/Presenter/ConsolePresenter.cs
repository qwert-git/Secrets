using Secrets.App.Models;

namespace Secrets.App.Services.Presenter;

internal class ConsolePresenter
{
    public void PresentAllKeys(IReadOnlyList<Secret> secrets)
    {
        for (var i = 0; i < secrets.Count; i++)
			Console.WriteLine($"{i + 1}. {secrets[i].Key}");
    }

    public void PresentSecret(Secret secret)
    {
        Console.WriteLine(secret is not null ? $"{secret.Key}: {secret.Login} {secret.Password}" : "Secret not found!");
    }
}