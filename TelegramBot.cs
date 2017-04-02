using System;
using System.IO;
using Telegram.Bot;

namespace deliciousfeed
{
    public class TelegramBotConfig
    {
        public static readonly TelegramBotClient Client = null;

        static TelegramBotConfig()
        {
            var key = Environment.GetEnvironmentVariable("TelegramBotKey");
            if (string.IsNullOrEmpty(key))
            {
                // if environment variable was not found, try reading from file.
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Secrets\Telegram-bot");
                if (File.Exists(filePath))
                {
                    key = File.ReadAllText(filePath);
                }
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("Telegram Bot Key is not specified.");
            }
            
            Client = new TelegramBotClient(key);
        }
    }
}