using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace deliciousfeed.Controllers
{
    public class BotController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"{DateTime.Now}\tOK");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            var message = update.Message;

            Console.WriteLine(string.Format("Received Message from {0}", message.Chat.Id));

            if (message.Type == MessageType.TextMessage)
            {
                Console.WriteLine(string.Format("Received Message: {0}", message.Text));

                // Echo each Message
                await TelegramBotConfig.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            else
            {
                Console.WriteLine($"Received Message Type: {message.Type}");
            }

            return Ok();
        }
    }
}
