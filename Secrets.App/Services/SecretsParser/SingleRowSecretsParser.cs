using Secrets.App.Exceptions;
using Secrets.App.Models;

namespace Secrets.App.Services;

class SingleRowSecretsParser : ISecretsParser
{
	private const char RowSeparator = '\n';
	private const int KeyRowIndex = 0;
	private const int LoginRowIndex = 1;
	private const int PasswordRowIndex = 2;

	private readonly string _separator;

	public SingleRowSecretsParser(string separator)
	{
		_separator = separator;
	}

	public ICollection<Secret> GetSecrets(string rawData)
	{
		return rawData
			.Split(RowSeparator)
			.Select(TransformToSecret)
			.ToList();
	}

	private Secret TransformToSecret(string row)
    {
		try
		{
			return ToSecret(row);
		}
		catch(Exception ex)
		{
			throw new SecretsAppException($"Wrong secrets raw data. Converting from the raw format failed with message `{ex.Message}`");
		}
    }

    private Secret ToSecret(string row)
    {
        var keyValuePair = row.Split(_separator);
        return new Secret
        {
            Key = keyValuePair[KeyRowIndex],
            Login = keyValuePair[LoginRowIndex],
            Password = keyValuePair[PasswordRowIndex]
        };
    }
}