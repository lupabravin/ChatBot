using Chat.Infrastructure.Models;

namespace Chat.Services.Interfaces
{
    public interface ICommandService
    {
        Message HandleCommand(string message);
    }
}
