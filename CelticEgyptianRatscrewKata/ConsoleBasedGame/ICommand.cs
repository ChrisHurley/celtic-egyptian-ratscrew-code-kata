namespace ConsoleBasedGame
{
    public interface ICommand
    {
        void Execute(ILogger log);
    }
}