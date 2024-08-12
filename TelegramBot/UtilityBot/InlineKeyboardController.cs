using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace UtilityBot
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        public Update update { get; private set; }
       
        public InlineKeyboardController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
            
        }
        public async Task Handle(CallbackQuery? callbackQuery, Update update, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            

            // Генерим информационное сообщение
            string languageText = callbackQuery.Data switch
            {
                "symbol" => " Количества символов в тексте",
                "sum" => " Сумма чисел",
                _ => String.Empty
            };

          //  await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Обнаружено нажатие на кнопку {callbackQuery.Data}", cancellationToken: ct);
            if (callbackQuery?.Data == "symbol")
            {
                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Введите сообщение для кнопки ", cancellationToken: ct);
                await _telegramClient.SendTextMessageAsync(update.Message.From.Id, $"Длина сообщения: {update.Message.Text.Length} знаков", cancellationToken: ct);

            }
            else if (callbackQuery?.Data =="sum")
            {

            }
            // Отправляем в ответ уведомление о выборе
            //  await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
            //  $"<b>Язык аудио - {languageText}.{Environment.NewLine}</b>" +
            //   $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
        public async Task SearchCh(Update update, CancellationToken cancellationToken)
        {


            switch (update.Message!.Type)
            {
                case MessageType.Text:
                    await _telegramClient.SendTextMessageAsync(update.Message.From.Id, $"Длина сообщения: {update.Message.Text.Length} знаков", cancellationToken: cancellationToken);
                    return;
                default:
                    await _telegramClient.SendTextMessageAsync(update.Message.From.Id, $"Данный тип сообщений не поддерживается. Пожалуйста отправьте текст.", cancellationToken: cancellationToken);
                    return;
            }



        }
    }
}
