using System;
using Newtonsoft.Json;
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

            Console.WriteLine("Received data from Telegram:");
            var jsonContent = JsonConvert.SerializeObject(update);
            Console.WriteLine(jsonContent);

            return Ok();
        }
    }
}
