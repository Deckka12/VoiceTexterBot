using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using VoiceTexterBot.Controllers;
using VoiceTexterBot.Services;
using VoiceTexterBot.Configuration;


namespace VoiceTexterBot {
    class Program {
        static async Task Main (string[] args) {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис


            await host.RunAsync();

            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices (IServiceCollection services) {
            AppSettings appSettings = BuildAppSettings();
            services.AddTransient<DefaultMessageController>();
            services.AddSingleton<InlineKeyboardController>();
            services.AddTransient<TextMessageController>();
            services.AddSingleton<VoiceMessageController>();
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<IFileHandler, AudioFileHandler>();

            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
        static AppSettings BuildAppSettings () {
            return new AppSettings() {
               // DownloadsFolder = "E:\\tg",
                BotToken = "5798862107:AAEDcdaBsR4HKsTDhLaDVIa4n64qM5HhoNw",
               /* AudioFileName = "audio",
                InputAudioFormat = "ogg",
                OutputAudioFormat = "wav",*/
                
            };
        }
    }

}