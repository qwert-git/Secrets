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

    public bool IsShowAllSecrets() => _args.Count == 0 || _args[0] == string.Empty;

    public Secret GetSecret(IReadOnlyList<Secret> secrets) => secrets.ElementAtOrDefault(GetKeyNumber()); 

    public bool IsGetSecret() => _args.Count > 0 && int.TryParse(_args[0], out _);

    public int GetKeyNumber() => int.Parse(_args[0]) - 1;
}