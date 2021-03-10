using Chat.CrossCutting.Helpers;
using Chat.WebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.WebApp.Bot
{
    public class Consumer : BackgroundService
    {
        string _rabbitConnection;
        private IHubContext<ChatHub> _hubContext;
        private CrossCutting.Consumer _consumer;
        public Consumer(IHubContext<ChatHub> hubContext, IConfiguration configuration) : base()
        {
            _rabbitConnection = Environment.GetEnvironmentVariable("RabbitMQConnection");
            if (string.IsNullOrEmpty(_rabbitConnection))
                _rabbitConnection = configuration.GetConnectionString("RabbitMQConnection");

            _hubContext = hubContext;
            _consumer = new CrossCutting.Consumer();
        }
        public void Consume()
        {
            _consumer.Consume<string>(BotHelper.BOT_MESSAGES_QUEUE, _rabbitConnection, async result =>
            {
                await _hubContext.Clients.All.SendAsync("GetMessage", "-1", "ChatBot", result, DateTime.Now.FormatDate());
            });
        }

        protected override Task ExecuteAsync(CancellationToken stopToken)
        {
            stopToken.ThrowIfCancellationRequested();
            Consume();
            return Task.CompletedTask;
        }

    }
}
