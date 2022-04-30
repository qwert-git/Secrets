using Secrets.App.Models;

namespace Secrets.Services.CommandTranslator;

internal class ConsoleCommandTranslator : ICommandTranslator
{
    private IReadOnlyList<string> _args;

    public ConsoleCommandTranslator(IReadOnlyList<string> args)
    {
        _args = args;
    }

    public bool IsShowAllSecrets() => _args.Count == 0 || _args[0] == string.Empty;

    /// <inheritdoc />
    public bool IsRemove() => _args.Count == 2 && (_args[0] == "rm" || _args[0] == "remove") && int.TryParse(_args[1], out _);

    public Secret GetSecret(IReadOnlyList<Secret> secrets) => secrets.ElementAtOrDefault(GetAddKeyNumber());

    public bool IsGetSecret() => _args.Count > 0 && int.TryParse(_args[0], out _);

    public int GetAddKeyNumber() => int.Parse(_args[0]) - 1;
    
    public int GetRemoveKeyNumber() => int.Parse(_args[1]) - 1;

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