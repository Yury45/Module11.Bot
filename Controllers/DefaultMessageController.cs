using Module11.Bot.Controllers.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Module11.Bot.Controllers
{
    internal class DefaultMessageController : IMessageController
    {
        private ITelegramBotClient _telegramClient;

        public DefaultMessageController(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        internal async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение не поддерживаемого формата", cancellationToken: ct);
        }
    }
}
