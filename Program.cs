using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Module11.Bot.Configuration;
using Module11.Bot.Controllers;
using Module11.Bot.Models;
using Module11.Bot.Services;
using Module11.Bot.Services.Interfaces;
using Telegram.Bot;

namespace Module11.Bot
{
    internal class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<DefaultMessageController>();
 
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddTransient<ICalculator, CalculationService>();
            services.AddTransient<MessageLenghtService>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<TelegramBot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = ""
            };
        }
    }
}
