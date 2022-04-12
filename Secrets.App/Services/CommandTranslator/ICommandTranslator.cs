using Secrets.App.Models;

namespace Secrets.Services.CommandTranslator;

internal interface ICommandTranslator
{
    int GetKeyNumber();
    Secret GetNewSecret();
    Secret GetSecret(IReadOnlyList<Secret> secrets);
    bool IsAddNew();
    bool IsGetSecret();
    bool IsInitEncryptionFile();
    bool IsShowAllSecrets();
    bool IsRemove();
}
