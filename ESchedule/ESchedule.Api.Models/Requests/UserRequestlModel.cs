using ESchedule.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ESchedule.Api.Models.Requests
{
    public record UserRequestModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FatherName { get; set; } = null!;

        public Role Role { get; set; }

        [Range(5, 99)]
        public int Age { get; set; }

        public string Password { get; set; } = null!;

        public string Login { get; set; } = null!;
    }
}
