﻿using Chat.Infrastructure.Models;
using Chat.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private ChatContext _chatContext;
        public MessageRepository(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }
        public List<Message> GetMessages(int quantity)
        {
            return _chatContext.Messages?
                    .Include(m => m.Sender)
                    .OrderBy(m => m.Date)
                    .Take(quantity).ToList();
        }

        public Message Add(Message message)
        {
            _chatContext.Add(message);
            _chatContext.SaveChanges();

            return message;
        }
    }
}
