using ESchedule.Domain.Users;

namespace ESchedule.Domain
{
    public record UserCredentialsModel : BaseModel
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsEmailConfirmed { get; set; }
    }
}
