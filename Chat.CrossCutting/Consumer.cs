using Chat.CrossCutting.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Chat.CrossCutting
{
    public class Consumer : IConsumer, IDisposable
    {
        IConnection _connection;
        IModel _channel;
        public void Consume<T>(string targetQueue, string rabbitConnection, Action<T> callback)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(rabbitConnection) };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: targetQueue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                object result;
                if (typeof(T) == typeof(string))
                    result = Encoding.UTF8.GetString(body).Replace("\"", "");
                else
                    result = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body));

                callback((T)result);
            };

            consumer.Registered += OnConsumerRegistered;
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCanceled;

            _channel.BasicConsume(queue: targetQueue,
                                 autoAck: true,
                                 consumer: consumer);

        }

        private void OnConsumerCanceled(object sender, ConsumerEventArgs e) { }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
