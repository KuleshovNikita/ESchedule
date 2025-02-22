using ESchedule.Business.Email.Client;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ESchedule.Business.Email;

public class EmailService(
    IConfiguration config,
    IEmailMessageClient messageClient,
    ILogger<EmailService> logger
)
    : IEmailService
{
    public async Task SendConfirmEmailMessage(UserModel userModel)
    {
        logger.LogInformation("Sending email confirmation message for user {id}", userModel.Id);

        var confirmUrl = BuildConfirmUrl(userModel);
        var message = BuildEmailMessage(confirmUrl);
        await messageClient.SendEmail(message, userModel.Login);

        logger.LogInformation("Registration confirmation message sent successfully for user {id}", userModel.Id);
    }

    private string BuildConfirmUrl(UserModel userModel)
    {
        var serverUrl = config.GetSection("ClientUrl").Value;

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