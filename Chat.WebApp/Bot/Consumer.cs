using Chat.CrossCutting.Helpers;
using Chat.CrossCutting.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebApp.Bot
{
    public class Consumer : CrossCutting.Consumer
    {
        public string Consume()
        {
            return Consume<string>(BotHelper.CHAT_COMMANDS_QUEUE);
        }
    }
}
