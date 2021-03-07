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
        private IChatService _chatService;
        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }
        public async Task PostMessage(string user, string message)
        {
            //  if (message.StartsWith("/"))


            //_chatService.Add(new Message
            //{
            //    Text = message,
                
            //});

            await Clients.All.SendAsync("GetMessage", user, message);
        }
    }
}
