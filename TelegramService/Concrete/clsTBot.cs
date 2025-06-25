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
        private readonly IDictionary<string, Func<Update, Task>> _actions = new Dictionary<string, Func<Update, Task>>();

        public clsTBot(ITelegramBotClient client)
        {
            _client = client;
            StartReceivingAsync();
        }

        public void AddAction(string key, Func<Update, Task> action) => _actions[key] = action;



        public void StartReceivingAsync(CancellationToken cancellationToken = default)
        {
            _client.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                cancellationToken: cancellationToken
            );
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message || update.Message?.Text == null)
                return;

            var messageText = update.Message.Text.Trim();

            if (_actions.TryGetValue(messageText, out var action))
            {
                await action.Invoke(update);
            }

        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task SendMessageAsync(string chatId, string message)
        {
            try
            {
                await _client.SendMessage(chatId, message, ParseMode.Markdown);
            }
            catch { }
        }

    }
}
