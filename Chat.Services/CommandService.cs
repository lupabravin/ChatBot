using Chat.Infrastructure.Models;
using Chat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class CommandService : ICommandService
    {
        public CommandService()
        {

        }

        public Message HandleCommand(string message)
        {
            return new Message();
        }
    }
}
