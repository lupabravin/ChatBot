using Chat.CrossCutting.Helpers;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Chat.Bot
{
    class Program
    {
        static void Main(string[] args)
        {

            string rabbitConnection = Environment.GetEnvironmentVariable("RabbitMQConnection");
            if (string.IsNullOrEmpty(rabbitConnection))
            {
                var builder =
                    new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();
                rabbitConnection = configuration.GetConnectionString("RabbitMQConnection");
            }

            var producer = new Producer(rabbitConnection);
            var consumer = new Consumer(rabbitConnection, producer);

            while (true)
                consumer.Consume(BotHelper.CHAT_COMMANDS_QUEUE);

        }
    }
}
