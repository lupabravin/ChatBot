using Chat.Infrastructure.Models;
using Chat.Repository;
using Chat.Repository.Interfaces;
using Chat.Services;
using Chat.Services.Interfaces;
using Chat.Tests.Factories;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chat.Tests.Services
{
    public class MessageServiceTests
    {
        public IMessageService _messageService;

        [Fact]
        public void Message_Add_ShouldReturnTrue()
        {
            //Arrange  
            ChatContext context;
            _messageService = CreateFakeService(out context);

            var user = new AppUser
            {
                Id = "Test UserId",
                UserName = "Test UserName"
            };

            context.AppUsers.Add(user);
            context.SaveChanges();

            var message = new Message()
            {
                Date = DateTime.Now,
                Text = "Test Text",
                UserId = user.Id,
                MessageId = 1,
                Sender = user
            };

            //Act  
            var insertedMessage = _messageService.Add(message.UserId, message.Sender.UserName, message.Text);
            var newMessage = context.Messages.FirstOrDefault(m => m.MessageId == message.MessageId);
            //newMessage.Date = insertedMessage.Date;

            //Assert 
            newMessage.Should().BeEquivalentTo(insertedMessage);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        [InlineData(70)]
        [InlineData(100)]
        public void Message_GetMessages_ShouldReturnMessagesQuantity(int quantity)
        {
            //Arrange  
            ChatContext context;
            _messageService = CreateFakeService(out context);

            var messagesList = new List<Message>();

            for (int i = 1; i <= quantity; i++)
            {
                var user = new AppUser
                {
                    Id = $"Test UserId {i}",
                    UserName = $"Test UserName {i}"
                };

                context.AppUsers.Add(user);

                var message = new Message()
                {
                    Text = $"Test Text {i}",
                    UserId = user.Id,
                    MessageId = i,
                    Sender = user
                };

                var newMessage = _messageService.Add(user.Id, user.UserName, message.Text);
                message.Date = newMessage.Date;
                messagesList.Add(message);

            }

            context.SaveChanges();
            var listComparer = messagesList.Skip(Math.Max(0, messagesList.Count() - quantity)).ToList();

            //Act  

            var messages = _messageService.GetMessages(quantity);

            //Assert 

            Assert.True(messages.Count == quantity);
            messages.Should().BeEquivalentTo(listComparer);

        }

        public IMessageService CreateFakeService(out ChatContext context)
        {
            var factory = new ConnectionFactory();
            context = factory.CreateTestContext();
            var messageRepository = new MessageRepository(context);
            var userRepository = new UserRepository(context);
            var messageService = new MessageService(messageRepository, userRepository);

            return messageService;
        }
    }
}
