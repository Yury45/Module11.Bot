using Module11.Bot.Controllers.Interfaces;
using Module11.Bot.Services;
using Module11.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Module11.Bot.Controllers
{
    internal class TextMessageController : IMessageController
    {
        private ITelegramBotClient _telegramBotClient;
        private readonly IStorage _memoryStorage;
        private readonly ICalculator _calculationService;
        private readonly MessageLenghtService _messageLenghtService;

        public TextMessageController(ITelegramBotClient telegramBotBotClient, 
            IStorage memoryStorage, 
            ICalculator calculationService,
            MessageLenghtService messageLenghtService)
        {
            _telegramBotClient = telegramBotBotClient;
            _memoryStorage = memoryStorage;
            _calculationService = calculationService;
            _messageLenghtService = messageLenghtService;
        }

        internal async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
           

            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();

                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Сложение", $"Calculator"), 
                        InlineKeyboardButton.WithCallbackData($"Счетчик символов", $"Counter") 
                    });

                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b>Выберите режим:</b> ", cancellationToken: ct, parseMode: ParseMode.Html);
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b>Сложение</b> <i> - Бот может посчитать сумму чисел, написанных через пробел.</i>" +
                        $"{Environment.NewLine}<b>Счетчик символов</b><i> - Бот подсчитает количество символов в сообщении.</i>", 
                        cancellationToken: ct,
                        parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;

                default:
                    if (_memoryStorage.GetSession(message.Chat.Id).Position == "Calculator")
                    {
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                            _calculationService.Add(message.Text),
                            cancellationToken: ct,
                            parseMode: ParseMode.Html);
                        break;
                    }

                    if (_memoryStorage.GetSession(message.Chat.Id).Position == "Counter")
                    {
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                            _messageLenghtService.MessageLenght(message.Text),
                            cancellationToken: ct,
                            parseMode: ParseMode.Html);
                        break;
                    }

                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b>Выберите режим: /start</b> ", cancellationToken: ct, parseMode: ParseMode.Html);
                    break;
            }
        }
    }
}