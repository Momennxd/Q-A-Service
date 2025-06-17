using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramService.Interfaces;
namespace TelegramService.Concrete
{
    public class clsTBot : ITelegramBot
    {
        private readonly ITelegramBotClient _client;

        public clsTBot(ITelegramBotClient client)
        {
            _client = client;
        }

        //public async Task StartReceivingAsync(CancellationToken cancellationToken = default)
        //{
        //    _client.StartReceiving(
        //        HandleUpdateAsync,
        //        HandleErrorAsync,
        //        cancellationToken: cancellationToken
        //    );
        //}

        //private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        //{
        //    if (update.Type == UpdateType.Message && update.Message!.Text != null)
        //    {
        //        var chatId = update.Message.Chat.Id;
        //        var messageText = update.Message.Text;

        //        await botClient.SendMessage(
        //            chatId: chatId,
        //            text: $"انت بعت: {messageText}  على chatID: {chatId}",
        //            cancellationToken: cancellationToken
        //        );
        //    }
        //}

        //private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        //{
        //    var errorMessage = exception switch
        //    {
        //        ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        //        _ => exception.ToString()
        //    };

        //    Console.WriteLine(errorMessage);
        //    return Task.CompletedTask;
        //}

        public async Task SendMessageAsync(string chatId, string message)
        {
            try
            {
                await _client?.SendMessage(chatId, message);
            }
            catch { }
        }

    }
}
