using ESchedule.Domain.Enums;

namespace ESchedule.Api.Models.Requests.Create.Users;

public record UserCreateModel
{
    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public int Age { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public Guid? TenantId { get; set; } = null!;
}
