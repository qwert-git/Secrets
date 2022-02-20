namespace Secrets
{
	public class AppConfig
	{
		public string OriginFilePath { get; set; }

		public string EncryptedFilePath { get; set; }

		public string EncryptionKey { get; set; }

		public string RowSeparator { get; set; }
	}
}