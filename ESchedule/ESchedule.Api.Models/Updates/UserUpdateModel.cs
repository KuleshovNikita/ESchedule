using ESchedule.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ESchedule.Api.Models.Updates
{
    public record UserUpdateModel : BaseUpdateModel
    {
        public string? Name { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? FatherName { get; set; } = null!;

        [Range(5, 99)]
        public int? Age { get; set; } = null!;

        public string? Login { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public Role? Role { get; set; } = null!;

        public Guid? GroupId { get; set; } = null!;

        public Guid? TenantId { get; set; } = null!;
    }
}
