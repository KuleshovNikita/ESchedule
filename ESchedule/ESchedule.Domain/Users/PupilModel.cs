namespace ESchedule.Domain.Users
{
    public record PupilModel : BaseUserModel
    {
        public GroupModel Group { get; set; } = null!;
    }
}
