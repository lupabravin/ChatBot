using Chat.Infrastructure.Models;
using System.Linq;


namespace Chat.Repository.Interfaces
{
    public class UserRepository : IUserRepository
    {
        ChatContext _chatContext;
        public UserRepository(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public AppUser GetUserById(string userId)
        {
            return _chatContext.AppUsers.FirstOrDefault(u => u.Id == userId);
        }
    }
}
