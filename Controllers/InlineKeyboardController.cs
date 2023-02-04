using Module11.Bot.Controllers.Interfaces;
using Module11.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Module11.Bot.Controllers
{
    internal class InlineKeyboardController : IInlineKeyboardController
    {
        private ITelegramBotClient _telegramBotClient;
        private IStorage _memoryStirage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStirage = memoryStorage;
        }

        internal async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if(callbackQuery.Data == null) return; 
            _memoryStirage.GetSession(callbackQuery.From.Id).Position = callbackQuery.Data;
            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id, $"Выбран режим - {callbackQuery.Data}", cancellationToken: ct);
        }
    }
}
