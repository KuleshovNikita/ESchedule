namespace ESchedule.Api.Models.Updates
{
    public record UserUpdateRequestModel
    {
        public Guid? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FatherName { get; set; }

        public int? Age { get; set; }

        public string? Password { get; set; }

        public string? Login { get; set; }
    }
}
