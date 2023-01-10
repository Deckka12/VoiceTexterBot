using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceTexterBot.Services;

namespace VoiceTexterBot.Controllers {
    class InlineKeyboardController {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memorystorage;
        public InlineKeyboardController (ITelegramBotClient telegramClient, IStorage memoryStoradge) {
            _telegramClient = telegramClient;
            _memorystorage = memoryStoradge;
        }
        public async Task Handle (CallbackQuery? callbackQuery, CancellationToken ct) {
            if(callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memorystorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;
            // Генерим информационное сообщение
            string languageText = callbackQuery.Data switch {
                "RU" => " Русский",
                "EN" => " Английский",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Язык аудио - {languageText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
