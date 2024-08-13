using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Settings;


namespace UtilityBot
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly AppSettings _appSetiings;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, AppSettings appSetiings)
        {
            _telegramClient = telegramBotClient;
            _appSetiings = appSetiings;

        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
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

                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Введите сообщение для команды 1", cancellationToken: ct);
                Console.WriteLine("Ожидаем 10 секунд.");
                Thread.Sleep(10000);
                _appSetiings.num = 1;


            }
            else if (callbackQuery?.Data == "sum")
            {
                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Введите сообщение для команды 2 (Пример ввода: 12 8 10)", cancellationToken: ct);
                Console.WriteLine("Ожидаем 10 секунд.");
                _appSetiings.num = 2;
            }


        }
    }
}

