using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Runtime;



namespace UtilityBot
{
    public class Bot : BackgroundService
    {

        private ITelegramBotClient _telegramClient;
        // Контроллер обработки сообщения видов сообщений

        private TextMessageController _textMessageController;
        private InlineKeyboardController _inlineKeyboardController;
        

        public Bot(ITelegramBotClient telegramClient, TextMessageController textMessageController, 
            InlineKeyboardController inlineKeyboardController)
        {


            _telegramClient = telegramClient;
            _textMessageController = textMessageController;
            _inlineKeyboardController = inlineKeyboardController;   
           
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } }, // Здесь выбираем, какие обновления хотим получать. В данном случае разрешены все
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");

        }



        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
                return;
            }
            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {

                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, cancellationToken);
                       
                        return;
                    
                    default:
                        Console.WriteLine("Тип сообщения по defualt и не выбран");
                        await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Тип сообщения по defualt и не выбран"); 
                    return;
                }





            }
           

        }
        //Обработчик ошибок
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            // Выводим в консоль информацию об ошибке
            Console.WriteLine(errorMessage);

            // Задержка перед повторным подключением
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);
            return Task.CompletedTask;

        }
    }
}