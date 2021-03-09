using RabbitMQ.Client;
using System;
using System.Text;

namespace Chat.Bot
{
    public class Producer
    {
        IModel _channel;

        public Producer(IModel channel)
        {

        }

        public void Produce(string message)
        {
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: "",
                                     routingKey: "BotMessages",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

    }
}
