using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat.CrossCutting
{
    public class Consumer
    {
        public T Consume<T>(string targetQueue)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                T queueObject = default(T);
                channel.QueueDeclare(queue: targetQueue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    T queueObject = JsonSerializer.Deserialize<T>(body);
                };
                channel.BasicConsume(queue: targetQueue,
                                     autoAck: true,
                                     consumer: consumer);

                return queueObject;

            }
        }
    }
}
