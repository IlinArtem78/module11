using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Settings;

//для остановки бота


namespace UtilityBot
{
  
    public class TextMessageController
    {
        
        private readonly ITelegramBotClient _telegramClient;
        private readonly AppSettings _appSetiings;

        //   public CallbackQueryEventArgs callbackQueryEventArgs { get; set; }  
        public TextMessageController(ITelegramBotClient telegramBotClient, AppSettings appSetiings)
        {
            _telegramClient = telegramBotClient;
            _appSetiings = appSetiings;

        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var cts = new CancellationTokenSource(); //переменная для остановки бота
            
            int s = 0;
            string sum;
            string[] numbers; 
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
                case "/send2":
                    cts.Cancel();
                    Console.WriteLine("Бот остановлен");
                    break; 
                   
                default:
                    Console.WriteLine("Тип сообщения по defualt и не выбран");
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Тип сообщения по defualt и не выбран", cancellationToken: ct); 
                    break;
            }
            switch (_appSetiings.num)
            {
                case 1:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Количество символов {message.Text.Length}", cancellationToken: ct);
                break;
                case 2:
                    numbers = message.Text.Split(' ');  
                    foreach (var ch in numbers)
                    {
                        s += Convert.ToInt32(ch);
                    }
                    
                    sum = s.ToString();
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма элементов {sum}", cancellationToken: ct);
                    break;
            }









        }
      


    }
}

