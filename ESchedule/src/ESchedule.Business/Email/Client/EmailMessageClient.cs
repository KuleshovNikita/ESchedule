using ESchedule.Domain.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace ESchedule.Business.Email.Client;

public class EmailMessageClient(IConfiguration config, ILogger<EmailMessageClient> logger) : IEmailMessageClient
{
    private readonly IConfiguration _config = config;

    public async Task SendEmail(string message, string consumer)
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

        try
        {
            await client.SendMailAsync(sender!, consumer, Resources.ConfirmYourEmail, message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed sending registration confirmation email message");
            throw;
        }
    }
}