using Chat.CrossCutting.Helpers;
using Chat.Services;
using Chat.Services.Interfaces;
using RabbitMQ.Client;
using System;

namespace Chat.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: BotHelper.CHAT_COMMANDS_QUEUE,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueDeclare(queue: BotHelper.BOT_MESSAGES_QUEUE,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var consumer = new Consumer(channel);
                consumer.Consume();
            }
        }
    }
}
