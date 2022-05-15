namespace Secrets.App.Commands;

public class InitCommand : ICommand
{    
    public Task ExecuteAsync()
    {
        var encryptedFileName = AppContext.BaseDirectory + "/encrypted.json";
        if(!File.Exists(encryptedFileName))
        {
            File.Create(encryptedFileName);
        }

        return Task.CompletedTask;
    }
}