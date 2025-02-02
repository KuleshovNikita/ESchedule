namespace ESchedule.Domain
{
    public record AuthModel
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
