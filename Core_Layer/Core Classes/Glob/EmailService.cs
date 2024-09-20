using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.IdentityModel.Tokens;
using static Core_Layer.Glob.EmailService;


namespace Core_Layer.Glob
{
    public class EmailService
    {


        public class EmailSettings
        {
            public string SenderEmail { get; set; }
            public string MainEmailAppPass { get; set; }

            public EmailSettings(string SenderEmail, string MainEmailAppPass)
            {
                this.SenderEmail = SenderEmail;
                this.MainEmailAppPass = MainEmailAppPass;
            }
        }


        public static async Task<bool> SendEmailAsync(string ToMail, string Subject, string Body)
        {
            if (string.IsNullOrEmpty(ToMail))
                return false;

            // Read JSON file contents asynchronously
            using FileStream fs = new FileStream("JSONs/jsonEmailSettings.json",
                FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);

            // Deserialize JSON data into C# object asynchronously
            EmailSettings? emailSettings = await JsonSerializer.DeserializeAsync<EmailSettings>(fs);

            if (emailSettings == null)
                return false;

            MailMessage message = new MailMessage
            {
                From = new MailAddress(emailSettings.SenderEmail),
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(ToMail));

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailSettings.SenderEmail, emailSettings.MainEmailAppPass),
                EnableSsl = true,
            };

            try
            {
                await smtpClient.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                // Optionally log the exception or handle it accordingly
                return false;
            }
        }
    }
}
