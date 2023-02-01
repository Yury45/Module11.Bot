using Telegram.Bot.Types;

namespace Module11.Bot.Controllers.Interfaces
{
    internal interface IInlineKeyboardController
    {
        internal async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {

        }
    }
}
