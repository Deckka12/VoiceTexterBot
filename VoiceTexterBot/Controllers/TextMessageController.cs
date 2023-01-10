using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace VoiceTexterBot.Controllers {
    class TextMessageController {
        private readonly ITelegramBotClient _telegramClient;
        public TextMessageController (ITelegramBotClient telegramClient) {
            _telegramClient = telegramClient;
        }
        public async Task Handle (Message message, CancellationToken ct) {
            switch(message.Text) {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Русский",$"RU"),
                        InlineKeyboardButton.WithCallbackData($"English",$"EN")
                    });
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот превращает аудио в текст.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Можно записать сообщение и переслать другу, если лень печатать." +
                        $"{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Поулчил текстовое  сообщение", cancellationToken: ct);
                    break;
            }

        }
    }
}
