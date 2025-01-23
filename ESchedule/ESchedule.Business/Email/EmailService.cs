using ESchedule.Business.Email.Client;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;
using Microsoft.Extensions.Configuration;

namespace ESchedule.Business.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IEmailMessageClient _messageClient;

        public EmailService(IConfiguration config, IEmailMessageClient messageClient)
        {
            _config = config;
            _messageClient = messageClient;
        }

        public async Task SendConfirmEmailMessage(UserModel userModel)
        {
            var confirmUrl = BuildConfirmUrl(userModel);
            var message = BuildEmailMessage(confirmUrl);
            await _messageClient.SendEmail(message, userModel.Login);
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
    }
}
