using ESchedule.Domain.Users;

namespace ESchedule.Business.Email
{
    public interface IEmailService
    {
        Task SendEmailConfirmMessage(BaseUserModel userModel);
    }
}
