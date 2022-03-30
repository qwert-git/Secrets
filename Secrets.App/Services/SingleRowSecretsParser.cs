namespace Secrets.App.Services;

class SingleRowSecretsParser : ISecretsParser
{
	private readonly string _separator;

	public SingleRowSecretsParser(string separator)
	{
		_separator = separator;
	}

	public ICollection<(string secretKey, string login, string pswrd)> GetSecrets(string rawData)
	{
		return rawData
			.Split('\n')
			.Select(row =>
			{
				var keyValuePair = row.Split(_separator);
				return (keyValuePair[0], keyValuePair[1], keyValuePair[2]);
			})
			.ToList();
	}
}