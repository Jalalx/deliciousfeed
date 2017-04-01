using Telegram.Bot;

namespace deliciousfeed
{
    public class TelegramBotConfig
    {
        private const string Key = "343485790:AAFYmRcJOb_oGvSfib52g0ePDodRV_NtTGM";
        public static readonly TelegramBotClient Client = new TelegramBotClient(Key);
    }
}