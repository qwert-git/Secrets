using Secrets.App.Models;

namespace Secrets.App.Services;

internal class ConsoleCommandTranslator : ICommandTranslator
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

    public bool IsAddNew() => _args.Count > 0 && _args[0] == "add";

    public Secret GetNewSecret()
    {
        const int KeyArgsIndex = 1;
        const int LoginArgsIndex = 2;
        const int PasswordArgsIndex = 3;

        if (_args.Count < 4)
            return null;
        return new()
        {
            Key = _args[KeyArgsIndex],
            Login = _args[LoginArgsIndex],
            Password = _args[PasswordArgsIndex]
        };
    }
}