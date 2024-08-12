using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace UtilityBot
{
    public class TextMessageController
    {
      
        private readonly ITelegramBotClient _telegramClient;
        public Update update { get; set; }
        public CancellationToken cts { get; set; }
     //   public CallbackQueryEventArgs callbackQueryEventArgs { get; set; }  
        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
           
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Количества символов в тексте" , $"symbol"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел" , $"sum")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот  ведет подсчёт количества символов в тексте.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Если лень считать сумму, можно использовать, вторую кнопку.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));


                    break;
                
                   
                default:
                    ///  await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправьте аудио для превращения в текст.", cancellationToken: ct);
                    break;
            }

           








        }
      


    }
}

