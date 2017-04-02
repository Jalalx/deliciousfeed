using System.IO;
using Telegram.Bot;

namespace deliciousfeed
{
    public class TelegramBotConfig
    {
        public static readonly TelegramBotClient Client = null;

        static TelegramBotConfig()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Secrets\Telegram-bot");
            var key = File.ReadAllText(filePath);
            Client = new TelegramBotClient(key);
        }
    }
}