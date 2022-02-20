using CommandLine;

namespace Secrets.App.Caesar.Options
{
	public class Options
	{
		[Option('i', "inputFile", Required = true, HelpText = "Input file name with data. Full or relative.")]
		public string InputFile { get; set; }

		[Option('o', "outputFile", Required = true, HelpText = "Output file name to save data. Full or relative.")]
		public string OutputFile { get; set; }

		[Option('k', "key", Required = true, HelpText = "Encryption key.")]
		public string Key { get; set; }

		[Option('t', "operation", Required = true, HelpText = "Operation type. Encrypt = 0, Decrypt = 1")]
		public OperationType OperationType { get; set; }
	}
}