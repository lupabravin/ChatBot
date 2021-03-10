using Chat.Services.Interfaces;
using Chat.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Chat.WebApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IMessageService _messageService;

        public ChatController(ILogger<ChatController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var messages = _messageService.GetMessages(50);
            return View(messages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetMessages()
        {
            return View(_messageService.GetMessages(50));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
