using Telegram.Bot.Types;
using TelegramService.Interfaces;
using Prometheus;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace API_Layer.ChatOps
{
    public class TelegramChatOps
    {
        private readonly ITelegramBot _telegramBot;
        private readonly MetricsParser _metricsParser;
        HashSet<long> _admins;
        public TelegramChatOps(ITelegramBot telegramBot, MetricsParser metricsParser)
        {
            _telegramBot = telegramBot;
            _metricsParser = metricsParser;
            _admins = new HashSet<long> { 6372413670 };
            _init();
        }

        private void _init()
        {
            _telegramBot.AddAction("/metrics", _SendMetrics);
        }

        private async Task _SendMetrics(Update update)
        {
            var userId = update?.Message?.From?.Id;

            if (userId == null || !_admins.Contains(userId.Value))
                return;

            string FullName = update?.Message?.From?.FirstName + " " + update?.Message?.From?.LastName;
            var chatId = update?.Message?.Chat.Id.ToString() ?? string.Empty;

            var rawMetrics = await _GetMetricsDirectlyAsync();
            var message = "Mr. Ahmed, here are Metrics\n\n";
            message += string.IsNullOrEmpty(rawMetrics)
                ? "Metrics are not available at the moment."
                : _metricsParser.ParseAndSummarize(rawMetrics);

            await _telegramBot.SendMessageAsync(chatId, message);
        }

        private async Task<string> _GetMetricsDirectlyAsync()
        {
            using var stream = new MemoryStream();
            await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }
}
