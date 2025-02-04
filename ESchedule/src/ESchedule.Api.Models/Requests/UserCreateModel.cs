using ESchedule.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ESchedule.Api.Models.Requests
{
    public record UserCreateModel
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FatherName { get; set; } = null!;

        [Range(5, 99)]
        public int Age { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Role Role { get; set; }

        public Guid? TenantId { get; set; } = null!;
    }
}
