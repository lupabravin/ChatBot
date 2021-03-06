using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.CrossCutting.Helpers
{
    public static class BotHelper
    {
        public static Dictionary<string, string> COMMANDS = new()
        {
            { "/stock", "https://stooq.com/q/l/?s=#parameter&f=sd2t2ohlcv&h&e=csv" }
        };

        public const string CHAT_COMMANDS_QUEUE = "ChatCommands";
        public const string BOT_MESSAGES_QUEUE = "BotMessages";

        public static Uri GetRabbitMQConnectionString(IConfiguration config)
        {
            string rabbitConnection = Environment.GetEnvironmentVariable("RabbitMQConnection");
            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {

            }
            return new Uri(config["RabbitMQConnection"] ?? config.GetConnectionString("RabbitMQConnection"));
        }
    }
}
