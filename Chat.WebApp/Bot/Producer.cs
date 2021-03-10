using Chat.CrossCutting.Helpers;
using Chat.CrossCutting.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebApp.Bot
{
    public class Producer : CrossCutting.Producer
    {
        public void Produce(string command, string parameter, string targetQueue, string rabbitConnection)
        {
            var keyValuePair = KeyValuePair.Create(command, parameter);
            Produce(keyValuePair, targetQueue, rabbitConnection);
        }
    }
}

