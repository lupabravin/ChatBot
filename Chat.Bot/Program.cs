using Chat.CrossCutting.Helpers;
using System;
using System.IO;
using RabbitMQ.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace Chat.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
