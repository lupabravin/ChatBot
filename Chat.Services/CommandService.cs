using Chat.CrossCutting.Helpers;
using Chat.CrossCutting.Interfaces;
using Chat.Infrastructure.Models;
using Chat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services
{
    public class CommandService : ICommandService
    {
        IProducer _producer;
        public CommandService(IProducer producer)
        {
            _producer = producer;
        }

        public Message HandleCommand(string message)
        {
            Message msg = new Message { Sender = new AppUser { UserName = "Bot" } };
            try
            {
                var split = message.Split("=");
                var command = split[0];
                var parameter = split[1];

                if (!string.IsNullOrEmpty(command) && BotHelper.COMMANDS.Keys.Contains(command))
                {
                    if (!message.Contains('=') || string.IsNullOrEmpty(parameter))
                        throw new Exception(ErrorMessages.COMMAND_MISSING_PARAMETER.Replace("[command]", command));

                    _producer.Produce(command, parameter);
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
