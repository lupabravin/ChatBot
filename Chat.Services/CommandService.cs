using Chat.CrossCutting;
using Chat.CrossCutting.Helpers;
using Chat.CrossCutting.Interfaces;
using Chat.Infrastructure.Models;
using Chat.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Services
{
    public class CommandService : ICommandService
    {
        IProducer _producer;
        IConfiguration _configuration;
        string _rabbitConnection;

        public CommandService(IProducer producer, IConfiguration configuration)
        {
            _producer = producer;
            _configuration = configuration;
            _rabbitConnection = Environment.GetEnvironmentVariable("RabbitMQConnection") ?? _configuration?.GetConnectionString("RabbitMQConnection");
        }

        public Message HandleCommand(string message)
        {
            Message msg = new Message { Sender = new AppUser { UserName = "Bot" } };
            try
            {
                var split = message.Split("=");
                var command = split[0];
                var parameter = "";

                if (split.Length > 1)
                    parameter = split[1];

                if (!string.IsNullOrEmpty(command) && BotHelper.COMMANDS.Keys.Contains(command))
                {
                    if (!message.Contains('=') || string.IsNullOrEmpty(parameter))
                        throw new Exception(ErrorMessages.COMMAND_MISSING_PARAMETER.Replace("[command]", command));

                    var keyValuePair = KeyValuePair.Create(command, parameter);

                    _producer.Produce(keyValuePair, BotHelper.CHAT_COMMANDS_QUEUE, _rabbitConnection);
                }
                else
                    throw new Exception(ErrorMessages.UNKNOWN_COMMAND + message);


            }
            catch (Exception ex)
            {
                msg.Text = ex.Message;
            }

            return msg;
        }
    }
}
