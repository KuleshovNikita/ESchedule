using ESchedule.Domain;

namespace ESchedule.Business.Email
{
    public interface IEmailService
    {
        Task SendEmailConfirmMessage(UserCredentialsModel userModel);
    }
}
