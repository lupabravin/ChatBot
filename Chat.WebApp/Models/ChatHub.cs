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
        private ICommandService _commandService;
        public ChatHub(IMessageService messageService, ICommandService commandService)
        {
            _messageService = messageService;
            _commandService = commandService;
        }
        public async Task PostMessage(string userId, string userName, string message)
        {
            string date;
            Message msg;
            if (message.StartsWith("/"))       
                msg = _commandService.HandleCommand(message);      
            else
                msg = _messageService.Add(userId, userName, message);
               
            date = msg.Date.Hour.ToString().PadLeft(2, '0') + ":" +
                           msg.Date.Minute.ToString().PadLeft(2, '0') + " - " +
                           msg.Date.Month.ToString().PadLeft(2, '0') + "/" +
                           msg.Date.Day.ToString().PadLeft(2, '0') + "/" +
                           msg.Date.Year;

            await Clients.All.SendAsync("GetMessage", msg.UserId, msg.Sender.UserName, msg.Text, date);
        }
    }
}
