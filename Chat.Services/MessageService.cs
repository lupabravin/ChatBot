using Chat.Infrastructure.Models;
using Chat.Repository.Interfaces;
using Chat.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        public List<Message> GetMessages(int quantity)
        {
            return _messageRepository.GetMessages(quantity);
        }
        
        public Message Add(string userId, string userName, string text)
        {
            var user = _userRepository.GetUserById(userId);
            var message = new Message
            {
                Text = text,
                UserId = userId,
                Sender = user
            };

            _messageRepository.Add(message);
            return message;
        }
    }
}
