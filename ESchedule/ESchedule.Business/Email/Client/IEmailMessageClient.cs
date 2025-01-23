namespace ESchedule.Business.Email.Client
{
    public interface IEmailMessageClient
    {
        Task SendEmail(string message, string consumer);
    }
}
