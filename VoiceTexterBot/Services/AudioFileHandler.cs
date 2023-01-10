using VoiceTexterBot.Configuration;
using Telegram.Bot;
using VoiceTexterBot.Utilities;

namespace VoiceTexterBot.Services {
    public class AudioFileHandler : IFileHandler {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramBotClient;

        public AudioFileHandler (ITelegramBotClient telegramBotClient) {
            _telegramBotClient = telegramBotClient;
            //_appSettings = appSettings;
        }


        public async Task Download (string fileId, CancellationToken ct) {
            // Генерируем полный путь файла из конфигурации
            string inputAudioFilePath = Path.Combine("E:\\tg", $"audio.ogg");

            using(FileStream destinationStream = File.Create(inputAudioFilePath)) {
                // Загружаем информацию о файле
                var file = await _telegramBotClient.GetFileAsync(fileId, ct);
                if(file.FilePath == null)
                    return;

                // Скачиваем файл
                await _telegramBotClient.DownloadFileAsync(file.FilePath, destinationStream, ct);
            }
        }

        public string Process (string languageCode) {
            string inputAudioPath = Path.Combine("E:\\tg", $"audio.ogg");
            string outputAudioPath = Path.Combine("E:\\tg", $"audio.wav");

            Console.WriteLine("Начинаем конвертацию...");
            AudioConverter.TryConvert(inputAudioPath, outputAudioPath);
            Console.WriteLine("Файл конвертирован");

            Console.WriteLine("Начинаем распознавание...");
            var speechText = SpeechDetector.DetectSpeech(outputAudioPath, 72000, languageCode);
            Console.WriteLine("Файл распознан.");
            return speechText;
        }
    }
}
