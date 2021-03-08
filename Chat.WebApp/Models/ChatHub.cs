using Chat.Infrastructure.Models;
using Chat.Services.Interfaces;
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
        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task PostMessage(string userId, string userName, string message)
        {
            //  if (message.StartsWith("/"))

            _messageService.Add(userId, userName, message);
            await Clients.All.SendAsync("GetMessage", userId, userName, message);
        }
    }
}
