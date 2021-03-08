using Chat.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface ICommandService
    {
        Message HandleCommand(string message);
    }
}
