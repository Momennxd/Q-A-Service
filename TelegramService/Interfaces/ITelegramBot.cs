using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramService.Interfaces
{
    public interface ITelegramBot
    {
        /// <summary>
        /// Sends a message to a specific chat on Telegram.
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(string chatId, string message);
        //Task SendPhotoAsync(string chatId, string photoUrl, string caption = null);
        //Task SendDocumentAsync(string chatId, string documentUrl, string caption = null);
        //Task SendKeyboardAsync(string chatId, string message, IEnumerable<string> buttons);
        //Task SendInlineKeyboardAsync(string chatId, string message, IEnumerable<string> buttons);
        //Task SendLocationAsync(string chatId, double latitude, double longitude);
        //Task SendContactAsync(string chatId, string phoneNumber, string firstName);
        //Task SendPollAsync(string chatId, string question, IEnumerable<string> options);
    }
}
