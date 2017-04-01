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
    [Route("[controller]")]
    public class WebHookController : Controller
    {
        private static readonly List<string> _logs = new List<string>();
        public IActionResult Logs()
        {
            var result = string.Join("\r\n", _logs);
            return Ok(result);
        }

        public async Task<IActionResult> Post(Update update)
        {
            var message = update.Message;

            //Console.WriteLine("Received Message from {0}", message.Chat.Id);
            _logs.Add(string.Format("Received Message from {0}", message.Chat.Id));

            if (message.Type == MessageType.TextMessage)
            {
                _logs.Add(string.Format("Received Message: {0}", message.Text));

                // Echo each Message
                await TelegramBotConfig.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            else if (message.Type == MessageType.PhotoMessage)
            {
                // Download Photo
                var file = await TelegramBotConfig.Client.GetFileAsync(message.Photo.LastOrDefault()?.FileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = System.IO.File.Open(filename, FileMode.Create))
                {
                    await file.FileStream.CopyToAsync(saveImageStream);
                }

                await TelegramBotConfig.Client.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }

            return Ok();
        }
    }
}
