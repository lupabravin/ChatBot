using Chat.Infrastructure.Models;
using Chat.Services.Interfaces;
using Chat.WebApp.Helpers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.WebApp.Models
{
    public class ChatHub : Hub
    {
        private IMessageService _messageService;
        private ICommandService _commandService;
        public ChatHub(IMessageService messageService, ICommandService commandService)
        {
            _messageService = messageService;
            _commandService = commandService;
        }
        public async Task PostMessage(string userId, string userName, string message)
        {
            Message msg;
            if (message.StartsWith("/"))
            {
                await Push(userId, userName, message, DateTime.Now.FormatDate());
                msg = _commandService.HandleCommand(message);
            }
            else
                msg = _messageService.Add(userId, userName, message);
               
            await Push(msg.UserId, msg.Sender.UserName, msg.Text, msg.Date.FormatDate());
        }

        public async Task Push(string userId, string userName, string message, string date)
        {
            await Clients.All.SendAsync("GetMessage", userId, userName, message, date);
        }
    }
}
