using System;
using System.Threading.Tasks;
using CommandLine;
using Secrets.App.Caesar.Options;
using Secrets.Cryptography;
using Secrets.FileSystemIO;

await Parser.Default.ParseArguments<Options>(args)
	.WithParsedAsync(ParseArgumentsHandler);

async Task ParseArgumentsHandler(Options options)
{
	var fileReader = new FileDataReader(options.InputFile);
	var fileWriter = new FileDataWriter(options.OutputFile);

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

	await fileWriter.WriteAsync(outputData);
}

// TODO:
// 1. Add console output
// 2. Remove output file parameter
// 3. Add cli command of application
// 4. Maybe. Add input source as clipboard 