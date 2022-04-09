namespace Secrets.App.Commands;
internal interface ICommand
{
    public Task ExecuteAsync();
}
