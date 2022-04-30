using CommandLine;
using Secrets.App.Caesar.Options;
using Secrets.Cryptography;
using Secrets.FileSystemIO;

await Parser.Default.ParseArguments<Options>(args)
	.WithParsedAsync(ParseArgumentsHandler);

async Task ParseArgumentsHandler(Options options)
{
	var fileReader = new FileDataReader(options.InputFile);

	var inputData = await fileReader.ReadAsync();
	string outputData;

	switch (options.OperationType)
	{
		case OperationType.Encrypt:
		{
			var encryptor = new SymmetricDataEncryptor();
			outputData = await encryptor.EncryptAsync(options.Key, inputData);
			break;
		}
		case OperationType.Decrypt:
		{
			var decryptor = new SymmetricDataDecryptor();
			outputData = await decryptor.DecryptAsync(options.Key, inputData);
			break;
		}
		default:
			throw new ArgumentOutOfRangeException();
	}

	if(string.IsNullOrWhiteSpace(options.OutputFile))
	{
		Console.WriteLine("Output data: ");
		Console.WriteLine("=================================== \n");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(outputData);
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("\n===================================");
	}
	else
	{
		var fileWriter = new FileDataWriter(options.OutputFile);
		await fileWriter.WriteAsync(outputData);
	}
}