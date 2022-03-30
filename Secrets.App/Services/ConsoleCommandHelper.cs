using Secrets.App.Models;

namespace Secrets.App.Services;

internal class ConsoleCommandTranslator
{
    private IReadOnlyList<string> _args;

    public ConsoleCommandTranslator(IReadOnlyList<string> args)
    {
        _args = args;
    }

    public bool IsInitEncyrptionFile() => _args.Count == 1 && _args[0] == "new";

    public bool IsShowAllSecrets() => _args.Count == 0;

    public Secret GetSecret(IReadOnlyList<Secret> secrets) => secrets.ElementAtOrDefault(GetKeyNumber()); 

    private int GetKeyNumber() => int.Parse(_args[0]) - 1;
}