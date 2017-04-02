﻿using System;
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
        private static readonly List<string> _logs = new List<string>();

        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"OK\r\n{DateTime.Now}");
        }

        [HttpGet]
        public IActionResult Logs()
        {
            var result = string.Join("\r\n", _logs);
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return Ok("No logs to display.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            var message = update.Message;

            var logMessage = string.Format("Received Message from {0}", message.Chat.Id);
            Console.WriteLine(logMessage);
            _logs.Add(logMessage);

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
