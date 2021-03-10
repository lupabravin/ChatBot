using Chat.CrossCutting.Interfaces;
using Microsoft.Extensions.Logging;
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
            ILoggerFactory fac = new LoggerFactory();
            ILogger logger = new Logger<Producer>(fac);
            logger.LogInformation($"\n\n\n\n\n\n ----------------------------- Producer Crosscutting: {rabbitConnection} --------------------------- \n\n\n\n\n\n ");

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

