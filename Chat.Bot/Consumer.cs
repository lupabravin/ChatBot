using Chat.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Bot
{
    public class Consumer
    {
        IModel _channel;

        public Consumer(IModel channel)
        {
            _channel = channel;
        }

        public void Consume()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };
            _channel.BasicConsume(queue: "ChatCommands",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}

