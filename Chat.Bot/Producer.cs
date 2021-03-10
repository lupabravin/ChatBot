using RabbitMQ.Client;
using System;
using System.Text;

namespace Chat.Bot
{
    public class Producer : CrossCutting.Producer
    {
        string _rabbitConnection;
        public Producer(string rabbitConnection)
        {
            _rabbitConnection = rabbitConnection;
        }
        public void Produce(string message, string targetQueue)
        {
            Produce(message, targetQueue, _rabbitConnection);
        }
    }
}
