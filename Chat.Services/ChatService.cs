using Chat.Infrastructure.Models;
using Chat.Repository.Interfaces;
using Chat.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Chat.Services
{
    public class ChatService : IChatService
    {
        private readonly IMessageRepository _messageRepository;
        public ChatService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public List<Message> GetMessages(int quantity)
        {
            return _messageRepository.GetMessages(quantity);
        }
        
        public Message Add(Message message)
        {
            _messageRepository.Add(message);
            return message;
        }
    }
}
