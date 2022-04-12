using Secrets.App.Models;

namespace Secrets.App.Services;

internal interface ICommandTranslator
{
    int GetKeyNumber();
    Secret GetNewSecret();
    Secret GetSecret(IReadOnlyList<Secret> secrets);
    bool IsAddNew();
    bool IsGetSecret();
    bool IsInitEncyrptionFile();
    bool IsShowAllSecrets();
}
