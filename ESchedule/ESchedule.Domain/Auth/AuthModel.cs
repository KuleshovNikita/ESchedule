namespace ESchedule.Domain
{
    public record AuthModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
