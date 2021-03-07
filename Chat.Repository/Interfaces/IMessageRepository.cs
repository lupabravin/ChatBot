using Chat.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Repository.Interfaces
{
    public interface IMessageRepository
    {
        List<Message> GetMessages(int quantity);
        Message Add(Message message);
    }
}
