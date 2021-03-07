using Chat.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IChatService
    {
        List<Message> GetMessages(int quantity);
        Message Add(Message message);
    }
}
