using System.ComponentModel.DataAnnotations;

namespace ESchedule.Domain.Users
{
    public abstract record BaseUserModel : BaseModel
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FatherName { get; set; } = null!;

        [Range(5, 99)]
        public int Age { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
