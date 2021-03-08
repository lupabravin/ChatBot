using Chat.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
