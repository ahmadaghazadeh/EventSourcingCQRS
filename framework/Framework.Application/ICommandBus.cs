using System.Windows.Input;

namespace Framework.Application
{
    public interface ICommandBus
    {
        Task Dispatch<T>(T command) where T : ICommand;
    }
}