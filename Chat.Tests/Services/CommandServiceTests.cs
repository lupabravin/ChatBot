using Chat.CrossCutting;
using Chat.CrossCutting.Helpers;
using Chat.Infrastructure.Models;
using Chat.Repository;
using Chat.Repository.Interfaces;
using Chat.Services;
using Chat.Services.Interfaces;
using Chat.Tests.Factories;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chat.Tests.Services
{
    public class CommandServiceTests
    {
        public ICommandService _commandService;

        [Theory]
        [InlineData("/stock")]
        [InlineData("/st231ock")]
        [InlineData("/sscc")]
        [InlineData("/stock=")]
        public void Command_HandleCommand_ShouldReturnErrorMessage(string message)
        {
            //Arrange  
            _commandService = CreateFakeService(out _);
            
            var split = message.Split("=");
            var command = split[0];
            var parameter = "";

            if (split.Length > 1)
                parameter = split[1];

            var errorMessage = "";

            if (string.IsNullOrEmpty(command) || !BotHelper.COMMANDS.Keys.Contains(command))
                errorMessage = ErrorMessages.UNKNOWN_COMMAND + message;
            else if(!message.Contains('=') || string.IsNullOrEmpty(parameter))
                errorMessage = ErrorMessages.COMMAND_MISSING_PARAMETER.Replace("[command]", command);

            //Act
            var exceptionMessage = _commandService.HandleCommand(message);

            //Assert
            exceptionMessage.Text.Should().BeEquivalentTo(errorMessage);
        }


        public ICommandService CreateFakeService(out ChatContext context)
        {
            var factory = new ConnectionFactory();
            context = factory.CreateTestContext();
            var commandService = new CommandService(new Producer(), null);

            return commandService;
        }
    }
}
