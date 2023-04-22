using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailConfirmMessage(UserModel userModel)
        {
            var confirmUrl = BuildConfirmUrl(userModel);
            var message = BuildEmailMessage(confirmUrl);
            await SendEmail(message, userModel.Login);
        }

        private string BuildConfirmUrl(UserModel userModel)
        {
            var serverUrl = _config.GetSection("ClientUrl").Value;

            var encodedHashKey = Uri.EscapeDataString(userModel.Password);
            var confirmEndpoint = $"/confirmEmail/{encodedHashKey}";

            return serverUrl + confirmEndpoint;
        }

        private string BuildEmailMessage(string confirmUrl)
        {
            var messageTemplate = Resources.EmailMessageTemplate;
            var messageWithLink = string.Format(messageTemplate, confirmUrl);

            return messageWithLink;
        }

        private async Task SendEmail(string message, string consumer)
        {
            var host = _config.GetSection("EmailBotData:Host").Value;
            var sender = _config.GetSection("EmailBotData:BotMail").Value;
            var password = _config.GetSection("EmailBotData:Password").Value;

            var client = new SmtpClient
            {
                Port = 587,
                Host = host!,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender, password)
            };

            await client.SendMailAsync(sender!, consumer, Resources.ConfirmYourEmail, message);
        }
    }
}
