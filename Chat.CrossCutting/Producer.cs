using Chat.CrossCutting.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.CrossCutting
{
    public class Producer : IProducer
    {
        public void Produce<T>(T obj, string targetQueue, string rabbitConnection)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(rabbitConnection) };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: targetQueue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

                channel.BasicPublish(exchange: "",
                                     routingKey: targetQueue,
                                     basicProperties: null,
                                     body: body);
            }


        }
    }
}

