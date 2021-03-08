using Chat.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Repository.Interfaces
{
    public interface IUserRepository
    {
        AppUser GetUserById(string userId);
    }
}
