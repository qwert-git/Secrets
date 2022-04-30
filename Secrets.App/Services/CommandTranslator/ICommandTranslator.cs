using Secrets.App.Models;

namespace Secrets.Services.CommandTranslator;

internal interface ICommandTranslator
{
    int GetAddKeyNumber();
    Secret GetNewSecret();
    Secret GetSecret(IReadOnlyList<Secret> secrets);
    bool IsAddNew();
    bool IsGetSecret();
    bool IsShowAllSecrets();
    bool IsRemove();
    int GetRemoveKeyNumber();
}
