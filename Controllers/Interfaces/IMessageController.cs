using Telegram.Bot.Types;

namespace Module11.Bot.Controllers.Interfaces
{
    internal interface IMessageController
    {
        public async Task Handle(Message message, CancellationToken ct)
        {

        }
    }
}
